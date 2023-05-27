let vehicles = [];
getData();

async function getData() {
    await fetch('http://localhost:47322/vehicle')
        .then(x => x.json())
        .then(y => {
            vehicles = y;
            console.log(vehicles);
            display();
        });
}


function display() {
    document.getElementById('resultarea').innerHTML = "";
    vehicles.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.displayReg + "</td><td>"
            + t.manufacturer + "</td><td>"
            + t.model + "</td><td>"
            + t.length + "</td><td>"
            + `<button type="button" onclick="remove(${t.registration})">Delete</button>`
            + "</td></tr>";
        //console.log(t.registration)
    });
}
function create() {
    let registration = document.getElementById('vehiclereg').value;
    let displayReg = registration;
    let manufacturer = document.getElementById('vehiclemanufact').value;
    let model = document.getElementById('vehiclemodel').value;
    let length = document.getElementById('vehiclelength').value;

    fetch('http://localhost:47322/vehicle', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                registration: registration,
                displayReg: displayReg,
                manufacturer: manufacturer,
                model: model,
                length: length,
                registrationDate: Date.now
            }),})
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(registration) {
    fetch('http://localhost:47322/vehicle/' + registration, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
