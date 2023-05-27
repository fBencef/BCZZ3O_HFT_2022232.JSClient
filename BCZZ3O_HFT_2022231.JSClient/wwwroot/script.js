fetch('http://localhost:47322/vehicle')
    .then(x => x.json())
    .then(y => console.log(y));