var cachedData = JSON.parse(localStorage.getItem("userData")) || [];
var tableRef = document.getElementById("userTable").getElementsByTagName("tbody")[0];


function populateTable() {
  for (var i = 0; i < cachedData.length; i++) {
    var newRow = tableRef.insertRow();
    var cell1 = newRow.insertCell(0);
    var cell2 = newRow.insertCell(1);
    var cell3 = newRow.insertCell(2);
    var cell4 = newRow.insertCell(3);
    var cell5 = newRow.insertCell(4);
    cell1.textContent = cachedData[i].firstName;
    cell2.textContent = cachedData[i].lastName;
    cell3.textContent = cachedData[i].email;
    cell4.textContent = cachedData[i].dob;

    var deleteButton = document.createElement("button");
    deleteButton.textContent = "Delete";
    deleteButton.className = "delete-button";
    deleteButton.addEventListener("click", deleteRow.bind(null, i));
    cell5.appendChild(deleteButton);

    var updateButton = document.createElement("button");
    updateButton.textContent = "Update";
    updateButton.className = "update-button";
    updateButton.addEventListener("click", editRow.bind(null, i));
    cell5.appendChild(updateButton);
  }
}

function editRow(index) {
    var row = tableRef.rows[index];
    var cells = row.cells;
  
    for (var i = 0; i < cells.length - 1; i++) {
      var cell = cells[i];
      var currentValue = cell.textContent;
      var input = document.createElement("input");
      input.type = "text";
      input.value = currentValue;
  
      cell.innerHTML = "";
      cell.appendChild(input);
    }
  
    var updateButton = row.querySelector(".update-button");
    updateButton.textContent = "Save";
    updateButton.removeEventListener("click", editRow);
    updateButton.addEventListener("click", saveRow.bind(null, index+1));
  }
  


function saveRow(index) {
    var row = tableRef.rows[index];
    var cells = row.cells;
  
    for (var i = 0; i < cells.length - 1; i++) {
      var cell = cells[i];
      var input = cell.querySelector("input");
      var newValue = input.value;
      cell.innerHTML = newValue;
      cachedData[index][getColumnKey(i)] = newValue;
    }
    
    var updateButton = row.querySelector(".update-button");
    updateButton.textContent = "Update";
    updateButton.removeEventListener("click", saveRow);
    updateButton.addEventListener("click", editRow.bind(null, index));

    localStorage.setItem("userData", JSON.stringify(cachedData));
  }
  

function getColumnKey(index) {
  var keys = ["firstName", "lastName", "email", "dob"];
  return keys[index];
}

document.getElementById("registrationForm").addEventListener("submit", function(event) {
  event.preventDefault();

  var firstName = document.getElementById("firstName").value;
  var lastName = document.getElementById("lastName").value;
  var email = document.getElementById("email").value;
  var password = document.getElementById("password").value;
  var dob = document.getElementById("dob").value;

  var newRow = tableRef.insertRow();
  var cell1 = newRow.insertCell(0);
  var cell2 = newRow.insertCell(1);
  var cell3 = newRow.insertCell(2);
  var cell4 = newRow.insertCell(3);
  var cell5 = newRow.insertCell(4);
  cell1.textContent = firstName;
  cell2.textContent = lastName;
  cell3.textContent = email;
  cell4.textContent = dob;

  var deleteButton = document.createElement("button");
  deleteButton.textContent = "Delete";
  deleteButton.className = "delete-button";
  deleteButton.addEventListener("click", deleteRow.bind(null, cachedData.length));
  cell5.appendChild(deleteButton);

  var updateButton = document.createElement("button");
  updateButton.textContent = "Update";
  updateButton.className = "update-button";
  updateButton.addEventListener("click", editRow.bind(null, cachedData.length));
  cell5.appendChild(updateButton);

  var userData = {
    firstName: firstName,
    lastName: lastName,
    email: email,
    dob: dob
  };
  cachedData.push(userData);
  localStorage.setItem("userData", JSON.stringify(cachedData));

  document.getElementById("registrationForm").reset();
});

function deleteRow(index) {
    if (tableRef.rows.length > 1) {
      tableRef.deleteRow(index);
      cachedData.splice(index, 1); 
      localStorage.setItem("userData", JSON.stringify(cachedData));
    } else {
      var cells = tableRef.rows[0].cells;
      for (var i = 0; i < cells.length - 1; i++) {
        cells[i].innerHTML = "";
      }
      cachedData = [];
      localStorage.removeItem("userData");
    }
  }
  
populateTable();
