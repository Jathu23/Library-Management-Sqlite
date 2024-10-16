let lentrecode_table = document.getElementById('lentbooks_table');

async function showlentrecode(nic) {
    lentrecode_table.innerHTML = ` <tr>
    <th>lentId</th>
        <th>ISBN</th>
        <th>Copies</th>
        <th>Lent-Date</th>
        <th>Return_Date</th>
  </tr>
  `;

try {
    let recods = await fetchLentrecodes(nic)
    recods.forEach(rec => {
        lentrecode_table.innerHTML += `<tr>
        <td>${rec.id}</td>
        <td>${rec.isbn}</td>
        <td>${rec.copies}</td>
        <td>${rec.lentDate}</td>
        <td>${rec.returnDate}</td>
      </tr>`;
    });
} catch (error) {
    console.log(error);
}

}

async function fetchLentrecodes(nic) {
    try {
        let url = 'https://localhost:7182/api/Lent/GetRecordsby_Nic?nic=' + encodeURIComponent(nic);
        let response = await fetch(url);
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
        return data;

    } catch (error) {
        console.error("An error occurred:", error.message);
    }
}


let usernic =JSON.parse(localStorage.getItem("LoginNic"));
console.log(usernic);
showlentrecode(usernic);

