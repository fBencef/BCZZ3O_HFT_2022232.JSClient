let vehicles = [];
let drivers = [];
let shifts = [];
let ncresults = [];

let connection = null;
let showing = 'vehicle';

let vehicleIdToUpdate = '';
let driverToUpdate = -1;
let shiftToUpdate = -1;

getData();
setupSignalR();

function pagebuttonclicked(page) {

    showing = page;
    if (showing == 'vehicle') {
        document.getElementById('vehiclecatalogdiv').style.display = 'initial';
        document.getElementById('drivercatalogdiv').style.display = 'none';
        document.getElementById('shiftcatalogdiv').style.display = 'none';
        document.getElementById('noncrudsdiv').style.display = 'none';
    }
    if (showing == 'driver') {
        document.getElementById('vehiclecatalogdiv').style.display = 'none';
        document.getElementById('drivercatalogdiv').style.display = 'initial';
        document.getElementById('shiftcatalogdiv').style.display = 'none';
        document.getElementById('noncrudsdiv').style.display = 'none';
    }
    if (showing == 'shift') {
        document.getElementById('vehiclecatalogdiv').style.display = 'none';
        document.getElementById('drivercatalogdiv').style.display = 'none';
        document.getElementById('shiftcatalogdiv').style.display = 'initial';
        document.getElementById('noncrudsdiv').style.display = 'none';
    }
    if (showing == 'nc') {
        document.getElementById('vehiclecatalogdiv').style.display = 'none';
        document.getElementById('drivercatalogdiv').style.display = 'none';
        document.getElementById('shiftcatalogdiv').style.display = 'none';
        document.getElementById('noncrudsdiv').style.display = 'initial';
    }
    display();
}

//function vehiclebuttonclicked() {


//    showing = 'vehicle';
//    display();
//}

//function driverbuttonclicked() {
//    document.getElementById('vehiclecatalogdiv').style.display = 'none';
//    document.getElementById('drivercatalogdiv').style.display = 'initial';
//    document.getElementById('shiftcatalogdiv').style.display = 'none';

//    showing = 'driver';
//    display();
//}

//function shiftbuttonclicked() {


//    showing = 'shift';
//    display();
//}

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

    connection.on("DriverCreated", (user, message) => {
        getData();
    });

    connection.on("DriverDeleted", (user, message) => {
        getData();
    });

    connection.on("DriverUpdated", (user, message) => {
        getData();
    });

    connection.on("ShiftCreated", (user, message) => {
        getData();
    });

    connection.on("ShiftDeleted", (user, message) => {
        getData();
    });

    connection.on("ShiftUpdated", (user, message) => {
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
    await fetch('http://localhost:47322/driver')
        .then(x => x.json())
        .then(y => {
            drivers = y;
            //console.log(drivers);
            display();
        });
    await fetch('http://localhost:47322/shift')
        .then(x => x.json())
        .then(y => {
            shifts = y;
            console.log(shifts);
            display();
        });
}


function display() {
    if (showing == 'vehicle') {
        console.log("SHOW VEHICLE")
        document.getElementById('vehicleresultarea').innerHTML = "";
        vehicles.forEach(t => {
            document.getElementById('vehicleresultarea').innerHTML +=
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
    if (showing == 'driver') {
        console.log("SHOW DRIVER")
        document.getElementById('driverresultarea').innerHTML = "";
        drivers.forEach(t => {
            document.getElementById('driverresultarea').innerHTML +=
                "<tr><td>" + t.driverId + "</td><td>"
                + t.name + "</td><td>"
                + t.age + "</td><td>"
                + `<button type="button" onclick="remove(${t.driverId})">Delete</button>`
                + `<button type="button" onclick="showupdate(${t.driverId})">Update</button>`
                + "</td></tr>";
            //console.log(t.registration)
        });
    }
    if (showing == 'shift') {
        console.log("SHOW SHIFT")
        document.getElementById('shiftresultarea').innerHTML = "";
        shifts.forEach(t => {
            document.getElementById('shiftresultarea').innerHTML +=
                "<tr><td>" + t.shiftId + "</td><td>"
                + t.line + "/" + t.tour + "</td><td>"
                + t.fromYard + "</td><td>"
                + t.vehicleId + "</td><td>"
                + t.driverId + "</td><td>"
                //+ t.vehicles.find(k => k['registration'] == vehicleId)['displayReg'] + "</td><td>"
                //+ t.drivers.find(k => k['driverId'] == driverId)['name'] + "</td><td>"
                + `<button type="button" onclick="remove(${t.shiftId})">Delete</button>`
                + `<button type="button" onclick="showupdate(${t.shiftId})">Update</button>`
                + "</td></tr>";
            //console.log(t.registration)
        });
    }
    if (showing == 'nc') {
        fetch('http://localhost:47322/NC/AvgDriverAge')
            .then(x => x.json())
            .then(y => {
                console.log(y);
                document.getElementById('avgageresult').innerHTML = Math.round(y);
            });
    }
}

function showupdate(id) {
    if (showing == 'vehicle') {
        document.getElementById('vehicleupdateformdiv').style.display = 'flex';

        document.getElementById('vehicleregtoupdate').value = vehicles.find(t => t['registration'] == id)['registration']
        document.getElementById('displayregtoupdate').value = vehicles.find(t => t['registration'] == id)['displayReg']
        document.getElementById('vehiclemanufacttoupdate').value = vehicles.find(t => t['registration'] == id)['manufacturer']
        document.getElementById('vehiclemodeltoupdate').value = vehicles.find(t => t['registration'] == id)['model']
        document.getElementById('vehiclelengthtoupdate').value = vehicles.find(t => t['registration'] == id)['length']


        vehicleIdToUpdate = id;
    }
    if (showing == 'driver') {
        document.getElementById('driverupdateformdiv').style.display = 'flex';

        document.getElementById('driveridtoupdate').value = drivers.find(t => t['driverId'] == id)['driverId']
        document.getElementById('drivernametoupdate').value = drivers.find(t => t['driverId'] == id)['name']
        document.getElementById('driveragetoupdate').value = drivers.find(t => t['driverId'] == id)['age']


        driverToUpdate = id;
    }
    if (showing == 'shift') {
        document.getElementById('shiftupdateformdiv').style.display = 'flex';
                                 
        document.getElementById('shiftidtoupdate').value = shifts.find(t => t['shiftId'] == id)['shiftId']
        document.getElementById('shiftlinetoupdate').value = shifts.find(t => t['shiftId'] == id)['line']
        document.getElementById('shifttourtoupdate').value = shifts.find(t => t['shiftId'] == id)['tour']
        document.getElementById('shiftyardtoupdate').value = shifts.find(t => t['shiftId'] == id)['fromYard']
        document.getElementById('shiftvehicletoupdate').value = shifts.find(t => t['shiftId'] == id)['vehicleId']
        document.getElementById('shiftdrivertoupdate').value = shifts.find(t => t['shiftId'] == id)['driverId']


        shiftToUpdate = id;
    }

}
function create() {
    if (showing == 'vehicle') {
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
    if (showing == 'driver') {
        //let driverId = document.getElementById('vehiclereg').value;
        let name = document.getElementById('drivername').value;
        let age = document.getElementById('driverage').value;

        fetch('http://localhost:47322/driver', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify(
                {
                    name: name,
                    age: age
                }),
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getData();
            })
            .catch((error) => { console.error('Error:', error); });
    }
    if (showing == 'shift') {
        let line = document.getElementById('shiftline').value;
        let tour = document.getElementById('shifttour').value;
        let fromYard = document.getElementById('shiftyard').value;
        let vehicleId = document.getElementById('shiftvehicle').value;
        let driverId = document.getElementById('shiftdriver').value;

        fetch('http://localhost:47322/shift', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify(
                {
                    line: line,
                    tour: tour,
                    fromYard: fromYard,
                    vehicleId: vehicleId,
                    driverId: driverId
                }),
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getData();
            })
            .catch((error) => { console.error('Error:', error); });
    }
}

function update() {
    document.getElementById('vehicleupdateformdiv').style.display = 'none';
    document.getElementById('driverupdateformdiv').style.display = 'none';
    document.getElementById('shiftupdateformdiv').style.display = 'none';

    if (showing == 'vehicle') {
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
    if (showing == 'driver') {
        let name = document.getElementById('drivernametoupdate').value;
        let age = document.getElementById('driveragetoupdate').value;

        fetch('http://localhost:47322/driver', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify(
                {
                    driverId: driverToUpdate,
                    name: name,
                    age: age,
                }),
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getData();
            })
            .catch((error) => { console.error('Error:', error); });
    }
    if (showing == 'shift') {
        let line = document.getElementById('shiftlinetoupdate').value;
        let tour = document.getElementById('shifttourtoupdate').value;
        let fromYard = document.getElementById('shiftyardtoupdate').value;
        let vehicleId = document.getElementById('shiftvehicletoupdate').value;
        let driverId = document.getElementById('shiftdrivertoupdate').value;

        fetch('http://localhost:47322/shift', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify(
                {
                    shiftId: shiftToUpdate,
                    line: line,
                    tour: tour,
                    fromYard: fromYard,
                    vehicleId: vehicleId,
                    driverId: driverId
                }),
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getData();
            })
            .catch((error) => { console.error('Error:', error); });
    }


}

function remove(id) {

    if (showing == 'vehicle') {
        fetch('http://localhost:47322/vehicle/' + id, {
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
    if (showing == 'driver') {
        fetch('http://localhost:47322/driver/' + id, {
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
    if (showing == 'shift') {
        fetch('http://localhost:47322/shift/' + id, {
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


}

function getshiftsofdriver() {
    fetch('http://localhost:47322/NC/ShiftsOfDriver/' + document.getElementById('drivernameinput').value)
        .then(x => x.json())
        .then(y => {
            console.log(y);
            ncresults = y;
            document.getElementById('shiftsofdriverresults').innerHTML = '';
            ncresults.forEach(t => {
                document.getElementById('shiftsofdriverresults').innerHTML +=
                    t.line + "/" + t.tour + "<br>";
            });
        });
}

function getmodelsofmanufact() {
    fetch('http://localhost:47322/NC/ListModels/' + document.getElementById('manufactinput').value)
        .then(x => x.json())
        .then(y => {
            console.log(y);
            ncresults = y;
            document.getElementById('modelsofmanufactresults').innerHTML = '';
            ncresults.forEach(t => {
                document.getElementById('modelsofmanufactresults').innerHTML +=
                    t + "<br>";
            });
        });
}

function getdriversofvehicle() {
    fetch('http://localhost:47322/NC/ListDrivers/' + document.getElementById('vehicleinput').value)
        .then(x => x.json())
        .then(y => {
            console.log(y);
            ncresults = y;
            document.getElementById('driversofvehicleresults').innerHTML = '';
            ncresults.forEach(t => {
                document.getElementById('driversofvehicleresults').innerHTML +=
                    t.name + "<br>";
            });
        });
}

function getvehiclesofline() {
    fetch('http://localhost:47322/NC/VehiclesOnLine/' + document.getElementById('lineinput').value)
        .then(x => x.json())
        .then(y => {
            console.log(y);
            ncresults = y;
            document.getElementById('vehiclesonlineresults').innerHTML = '';
            ncresults.forEach(t => {
                document.getElementById('vehiclesonlineresults').innerHTML +=
                    t.displayReg
                    + " (" + t.manufacturer + " " + t.model + ") "
                    + "[" + t.length + "m]"
                    + "<br>";
            });
        });
}
