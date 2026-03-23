const make = document.getElementById('make');
const model = document.getElementById('model');
const year = document.getElementById('year');
const carsList = document.getElementById('carsList')

async function addCar() {
    const response = await fetch('/api/car', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            make: make.value.trim(),
            model: model.value.trim(),
            year: parseInt(year.value)
        })
    });

    if (!response.ok) {
        const data = await response.json();
        if (response.status === 400 && data.errors) {
            const messages = Object.values(data.errors).flat();
            displayErrors(messages);
            return;
        }
        displayErrors([data.error ?? 'Something went wrong']);
        return;
    }

    const created = await response.json();  
    await listCars();                        
}                                          
function displayErrors(messages) {
    const errorDiv = document.getElementById('error-container');
    errorDiv.innerHTML = messages.map(m => `<p style="color:red">${m}</p>`).join('');
}

async function listCars() {
    const response = await fetch('/api/car');
    const cars = await response.json();

    if (cars.length === 0) {
        carsList.innerHTML =
        `<div class="empty-state">
            <div class="icon">🚗</div> 
            <div>No cars yet. Add your first one! </div>
        </div>`;
        return;
    }
    carsList.innerHTML = cars.map(c => `
        <div id="car-${c.id}">
            <div class="car-info">
                <div class="car-name">${c.make} ${c.model}</div>
                <div class="car-year">${c.year}</div>
            </div>
            <div class="car-actions">
                <button class="btn-edit" onclick="editCar(${c.id}, '${c.make}', '${c.model}', ${c.year})">Edit</button>
                <button class="btn-delete" onclick="deleteCar(${c.id})">Delete</button>
            </div>
        </div>`).join('');
}

async function deleteCar(id) {
    await fetch(`api/car/${id}`, {
        method: 'DELETE'
    });
    await listCars();
}

async function editCar(id, currentMake, currentModel, currentYear) {
    const newMake = prompt('Make:', currentMake);
    const newModel = prompt('Model:', currentModel);
    const newYear = parseInt(prompt('Year:', currentYear));

    if (!newMake || !newModel || isNaN(newYear)) return;

    const response = await fetch(`/api/car/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ make: newMake, model: newModel, year: newYear })
    });
        if(!response.ok) {
        const data = await response.json();
        if (response.status === 400 && data.errors) {
            const messages = Object.values(data.errors).flat();
            displayErrors(messages);
            return;
        }
        displayErrors([data.error ?? 'Something went wrong']);
        return;
    }
    await listCars();
}

document.getElementById('saveCarBtn').addEventListener('click', addCar);

listCars();