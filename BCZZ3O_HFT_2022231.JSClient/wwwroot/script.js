let vehicles = [];
let connection = null;

let vehicleIdToUpdate = '';

getData();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:47322/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("VehicleCreated", (user, message) => {
        getData();
    });

    connection.on("VehicleDeleted", (user, message) => {
        getData();
    });

    connection.on("VehicleUpdated", (user, message) => {
        getData();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getData() {
    await fetch('http://localhost:47322/vehicle')
        .then(x => x.json())
        .then(y => {
            vehicles = y;
            //console.log(vehicles);
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
            + `<button type="button" onclick="showupdate(${t.registration})">Update</button>`
            + "</td></tr>";
        //console.log(t.registration)
    });
}

function showupdate(registration) {
    document.getElementById('updateformdiv').style.display = 'flex';

    document.getElementById('vehicleregtoupdate').value = vehicles.find(t => t['registration'] == registration)['registration']
    document.getElementById('displayregtoupdate').value = vehicles.find(t => t['registration'] == registration)['displayReg']
    document.getElementById('vehiclemanufacttoupdate').value = vehicles.find(t => t['registration'] == registration)['manufacturer']
    document.getElementById('vehiclemodeltoupdate').value = vehicles.find(t => t['registration'] == registration)['model']
    document.getElementById('vehiclelengthtoupdate').value = vehicles.find(t => t['registration'] == registration)['length']


    vehicleIdToUpdate = registration;
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
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error); });
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';

    //let displayReg = document.getElementById('displayregtoupdate').value;
    let manufacturer = document.getElementById('vehiclemanufacttoupdate').value;
    let model = document.getElementById('vehiclemodeltoupdate').value;
    let length = document.getElementById('vehiclelengthtoupdate').value;

    fetch('http://localhost:47322/vehicle', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                registration: vehicleIdToUpdate,
                displayReg: displayReg,
                manufacturer: manufacturer,
                model: model,
                length: length,
                registrationDate: registrationDate
            }),
    })
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
            getData();
        })
        .catch((error) => { console.error('Error:', error); });
}
