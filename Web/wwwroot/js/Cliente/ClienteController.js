let container = document.getElementById("contenedor");
let clienteLista = [];
let bancosEditar = [];

function consultar() {
    fetch("https://localhost:7097/Cliente/Consultar")
        .then((resp) => resp.json())
        .then(data => {
            clienteLista = data;
            console.log(data);
            renderList(data);
        });
}

function renderList(list) {
    while (container.firstChild) {
        container.removeChild(container.firstChild);
    }
    addAddCard();
    list.forEach(e => {
        let card = document.createElement("div");
        card.classList.add("card");
        card.classList.add("m-2");
        card.classList.add("w-25");
        card.innerHTML = renderObj(e.id, e.nombre, e.apellido, e.edad);
        container.appendChild(card);
    });
}

function addAddCard() {
    let fc = document.createElement('div');
    fc.classList.add("card");
    fc.classList.add("m-2");
    fc.classList.add("w-25");
    fc.innerHTML = `<div class="card-body">
                <div>
                    <h3 class="text-center">Agregar</h3>
                </div>
                <div>
                    <label class="form-label">Nombre</label>
                    <input type="text" class="form-control border-0" placeholder="nombrePrueba" id="addNombre">
                </div>
                <div>
                    <label class="form-label">Apellido</label>
                    <input type="text" class="form-control border-0" placeholder="apellidoPrueba" id="addApellido">
                </div>
                <div>
                    <label class="form-label">Edad</label>
                    <input type="number" class="form-control border-0" placeholder="1" id="addEdad">
                </div>
                <div class="row mt-2">
                    <div class="col-12">
                        <button class="btn btn-primary w-100" onclick="btnAgregar()">Agregar</button>
                    </div>
                </div>
            </div>`;
    container.appendChild(fc);
}

function renderObj(id, nombre, apellido, edad) {
    let obj = `
                <div class="card-body">
                    <input name="Id" value="${id}" hidden>
                    <div>
                        <label class="form-label">Nombre</label>
                        <input type="text" class="form-control border-0" value="${nombre}" id="nombre_${id}" >
                    </div>
                    <div>
                        <label class="form-label">Apellido</label>
                        <input type="text" class="form-control border-0" value="${apellido}" id="apellido_${id}">
                    </div>
                    <div>
                        <label class="form-label">Edad</label>
                        <input type="text" class="form-control border-0" value="${edad}" id="edad_${id}">
                    </div>
                    <div class="row mt-2">
                        <div class="col-6">
                            <button class="btn btn-primary w-100" onclick="btnEditar(${id})">Editar</button>
                        </div>
                        <div class="col-6">
                            <button class="btn btn-danger w-100" onclick="eliminar(${id})">Eliminar</button>
                        </div>
                    </div>
                </div>
            `;
    return obj;
}



function btnEditar(id) {
    let temp = getInfoCard(id);
    console.log(temp);
    fetch("https://localhost:7097/Cliente/Editar", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(temp)
    })
        .then(resp => resp.json())
        .then(data => {
            if (data.status) {
                alert("Modificado con exito");
                consultar();
            } else {
                alert(data.msg);
            }
        })
}
function getInfoCard(id) {
    let apellido = document.getElementById("apellido_" + id).value;
    let nombre = document.getElementById("nombre_" + id).value;
    let edad = document.getElementById("edad_" + id).value;
    return {
        'Id': id,
        'Nombre': nombre,
        'Apellido': apellido,
        'Edad': edad
    };
}

function btnAgregar() {
    let obj = {
        Id : 0,
        Nombre : document.getElementById("addNombre").value,
        Apellido : document.getElementById("addApellido").value,
        Edad: document.getElementById("addEdad").value,
    };
    console.log(obj);
    fetch("https://localhost:7097/Cliente/Agregar", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(obj)
    }).then(resp => resp.json())
        .then(data => {
            if (data.status) {
                alert("Elemento creado");
                consultar();
            } else {
                alert(data.msg);
            }
        })
}

function eliminar(id) {
    fetch("https://localhost:7097/Cliente/Delete", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(id)
    })
        .then(resp => resp.json())
        .then(data => {
            if (data.status) {
                alert("elemento eliminado");
                consultar();
            } else {

                alert(data.msg);
            }
        })
        .catch(err => console.log(err))
}

consultar();