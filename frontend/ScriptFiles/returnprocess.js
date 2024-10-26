let rec_table = document.getElementById('return_rec_table');
var custoninfodiv = document.getElementById('custominfo');
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
async function fetchSingleUserLend(NicNumber) {
    try {
        let url = 'https://localhost:7182/api/Lent/GetRecordsby_Nic?nic==' + encodeURIComponent(NicNumber);
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
async function fetchSingleUser(NicNumber) {
    try {
        let url = 'https://localhost:7182/api/User/GetByUser?nic=' + encodeURIComponent(NicNumber);
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
async function fetchlentbookby_user(nic) {
    try {
        let url = 'https://localhost:7182/api/Lent/Getlentbooks_nic?nic=' + encodeURIComponent(nic);
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
async function showUserDetails() {
    try {
        const NicNumber = document.getElementById('issue_userid').value;
        let pendingbooks = await fetchlentbookby_user(NicNumber);

        if (NicNumber) {
            const UserData = await fetchSingleUser(NicNumber);
            let displaydiv = document.getElementById('issue-user');

            displaydiv.children[0].children[1].innerHTML = UserData.fullName
            displaydiv.children[1].children[1].innerHTML = UserData.phoneNumber
            displaydiv.children[2].children[1].innerHTML = UserData.email
            displaydiv.children[3].children[1].innerHTML = UserData.joinDate
            displaydiv.children[4].children[1].innerHTML = pendingbooks
        
        }

    } catch (error) {
        console.log("error in show user details");
        console.log(error.message);
    }

}
async function showLendRecordDetails(nic) {
    try {
      let infos = await getCustomInfo(nic)
      custoninfodiv.innerHTML = ``;
      infos.forEach(rec => {
        custoninfodiv.innerHTML += ` <div>
                        <p>${rec.lentId}</p>
                        <p>${rec.bookTitle}</p>
                        <p>${rec.copies}</p>
                        <p>${rec.lentDate}</p>
                        <p>${rec.status}</p>
                 </div>  </br>  `;
   
        
      });
     

    } catch (error) {
        console.log("error ");
        console.log(error.message);
    }

}
async function getCustomInfo(userId) {
    try {
        const url = `https://localhost:7182/api/Lent/customInfo?userid=${userId}`;
        
        let response = await fetch(url);

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        let data = await response.json();
        console.log("Custom Info:", data);
        return data;

    } catch (error) {
        console.error("Error fetching custom info:", error.message);
    }
}





showReturnrecods();



document.getElementById('issue_userid').addEventListener('input', () => {
    showUserDetails();   
    showLendRecordDetails(document.getElementById('issue_userid').value);
});














