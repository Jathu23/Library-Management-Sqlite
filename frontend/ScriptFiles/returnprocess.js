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



document.getElementById('issue_userid').addEventListener('input', () => {
    showUserDetails();
})



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

async function showUserDetails() {
    try {
        const NicNumber = document.getElementById('issue_userid').value;

        if (NicNumber) {
            const UserData = await fetchSingleUser(NicNumber);
            console.log(UserData);
            console.log(UserData.fullName);
            console.log(UserData.phoneNumber);
            console.log(UserData.joinDate);
            console.log(UserData.email);
            let displaydiv = document.getElementById('issue-user');

            displaydiv.children[0].children[1].innerHTML = UserData.fullName
            displaydiv.children[1].children[1].innerHTML = UserData.phoneNumber
            displaydiv.children[2].children[1].innerHTML = UserData.email
            displaydiv.children[3].children[1].innerHTML = UserData.joinDate
            displaydiv.children[4].children[1].innerHTML = UserData.copies
            displaydiv.children[5].children[1].innerHTML = `${futureDate}`
        }

    } catch (error) {
        console.log("error in show user details");
        console.log(error.message);
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





async function showLendRecordDetails() {
    try {
        const NicNumber = document.getElementById('issue_userid').value;

        if (true) {
            const endData = await fetchSingleUserLend(NicNumber) ;
           

            let displaydiv = document.getElementById('issue-info');

            displaydiv.children[0].children[1].innerHTML = bookData.title
            displaydiv.children[1].children[1].innerHTML = bookData.author
            displaydiv.children[2].children[1].innerHTML = bookData.id
            displaydiv.children[3].children[1].innerHTML = bookData.publishYear
            displaydiv.children[4].children[1].innerHTML = bookData.copies
            displaydiv.children[5].children[1].innerHTML = `${futureDate}`


        }

    } catch (error) {
        console.log("error ");
        console.log(error.message);
    }

}
