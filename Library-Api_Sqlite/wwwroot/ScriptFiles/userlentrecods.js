let lentrecode_table = document.getElementById('lentbooks_table');

async function showlentrecode(nic) {
    lentrecode_table.innerHTML = ` <tr id="T_head">
    <th class="th_cl">lentId</th>
        <th class="th_cl">ISBN</th>
        <th class="th_cl">Copies</th>
        <th class="th_cl">Lent-Date</th>
        <th class="th_cl">Return_Date</th>
        <th class="th_cl">Status</th>
  </tr>
  `;

try {
    let recods = await fetchLentrecodes(nic)
    recods.forEach(rec => {
        lentrecode_table.innerHTML += `<tr id="tb_raw">
        <td class="td_id">${rec.id}</td>
        <td class="td_id">${rec.isbn}</td>
        <td class="td_id">${rec.lentcopies}</td>
        <td class="td_id">${new Date(rec.lentDate).toLocaleDateString()}</td>
        <td class="td_id">${ new Date(rec.returnDate).toLocaleDateString()}</td>
        <td class="td_id">${rec.status}</td>
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
showlentrecode(usernic);

