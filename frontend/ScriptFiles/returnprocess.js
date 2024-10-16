let rec_table = document.getElementById('return_rec_table');
async function fetchreturnrecods() {
    try {
        let response = await fetch('https://localhost:7182/api/Return/GetAllRecods');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
        return data;
    } catch (error) {
        console.error("An error occurred:", error.message);
    }
}

async function showReturnrecods() {``
    try {
        rec_table.innerHTML =`<tr>
        <th>Return Id</th>
        <th> User-NIC</th>
        <th>Lent-Id</th>
        <th>ISBN</th>
        <th>LentCopies</th>
        <th>ReturnCopies</th>
        <th>ReturnDate</th>
    </tr>`;

let rec = await fetchreturnrecods();
rec.forEach(r => {
    rec_table.innerHTML += `
    <tr>
            <td>${r.id}</td>
            <td>${r.usernic}</td>
            <td>${r.lentId}</td>
            <td>${r.isbn}</td>
            <td>${r.lentcopies}</td>
            <td>${r.returncopies}</td>
            <td>${r.returndate}</td>
        </tr>
    `;
});

        
    } catch (error) {
        console.log(error,error.message);
    }
}

showReturnrecods();
