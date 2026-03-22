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
    const created = await response.json();
    await listCars();
}

async function listCars() {
    const response = await fetch('/api/car');
    const cars = await response.json();
    console.log(cars);
    carsList.innerHTML = cars
        .map(c => `<div id="car-${c.id}"> <span>Make: ${c.make} Model: ${c.model} Year: ${c.year}</span>
        <button onclick="deleteCar(${c.id})">Delete</button>
        <button onclick="editCar(${c.id}, '${c.make}', '${c.model}', ${c.year})">Edit</button>
        </div>`)
        .join('');
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

    await fetch(`/api/car/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ make: newMake, model: newModel, year: newYear })
    });
    await listCars();
}

document.getElementById('saveCarBtn').addEventListener('click', addCar);

listCars();