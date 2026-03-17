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
        .map(c => `<div> Make: ${c.make}Model: ${c.model} Year: ${c.year} </div>`)
        .join('');
}
document.getElementById('saveCarBtn').addEventListener('click', addCar);

listCars();