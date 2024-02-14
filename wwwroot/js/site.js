const uri = 'api/moveset';
let todos = [];

function getItems() {
  fetch(uri)
    .then(response => response.json())
    .then(data => _displayItems(data))
    .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
  const addNameTextbox = document.getElementById('add-name');
  const addBeltRequiredTextbox = document.getElementById('add-belt-required');
  const genRanHex = size => [...Array(size)].map(() => Math.floor(Math.random() * 16).toString(16)).join('');


  const item = {
    id : genRanHex(24),
    beltRequired: addBeltRequiredTextbox.value.trim(),
    name: addNameTextbox.value.trim()
  };

  fetch(uri, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Access-Control-Allow-Origin':'*',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
    .then(response => response.json())
    .then(() => {
      getItems();
      addNameTextbox.value = '';
    })
    .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
  fetch(`${uri}/${id}`, {
    method: 'DELETE'
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
  const item = todos.find(item => item.id === id);
  
  document.getElementById('edit-name').value = item.name;
  document.getElementById('edit-id').value = item.id;
  // document.getElementById('edit-beltRequired').checked = item.beltRequired;
  document.getElementById('edit-beltRequired').value = item.beltRequired;
  document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
  const itemId = document.getElementById('edit-id').value;
  const item = {
    // id: parseInt(itemId, 10),
    id: itemId,
    // beltRequired: document.getElementById('edit-beltRequired').checked,
    beltRequired: document.getElementById('edit-beltRequired').value.trim(),
    name: document.getElementById('edit-name').value.trim()
  };

  fetch(`${uri}/${itemId}`, {
    method: 'PUT',
    headers: {
      'Accept': 'application/json',
      'Access-Control-Allow-Origin':'*',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to update item.', error));

  closeInput();

  return false;
}

function closeInput() {
  document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
  const name = (itemCount === 1) ? 'move' : 'moves';

  document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
  const tBody = document.getElementById('todos');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(item => {
    let beltRequiredCheckbox = document.createElement('input');
    beltRequiredCheckbox.type = 'checkbox';
    beltRequiredCheckbox.disabled = true;
    beltRequiredCheckbox.checked = item.beltRequired;

    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `displayEditForm("${item.id}")`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteItem("${item.id}")`);

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    // td1.appendChild(beltRequiredCheckbox);
    let textNode1 = document.createTextNode(item.name);
    td1.appendChild(textNode1);

    let td2 = tr.insertCell(1);
    let textNode2 = document.createTextNode(item.beltRequired);
    td2.appendChild(textNode2);

    let td3 = tr.insertCell(2);
    td3.appendChild(editButton);

    let td4 = tr.insertCell(3);
    td4.appendChild(deleteButton);
  });

  todos = data;
}