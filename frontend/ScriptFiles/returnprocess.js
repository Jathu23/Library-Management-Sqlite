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
async function showReturnrecods() {
    ``
    try {
        rec_table.innerHTML = `<tr>
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
        console.log(error, error.message);
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
            var displaydiv = document.getElementById('issue-user');
            displaydiv.innerHTML = `  <div class="info">
                        <h4>Full Name</h4>
                        <p id="full-name">${UserData.fullName}</p>
                    </div>
                    <div class="info">
                        <h4>Phone Number</h4>
                        <p id="user-phone">${UserData.phoneNumber}</p>
                    </div>
                    <div class="info">
                        <h4>Email</h4>
                        <p id="user-email">${UserData.email}</p>
                    </div>
                    <div class="info">
                        <h4>Join Date</h4>
                        <p id="user-join-date">${UserData.joinDate}</p>
                    </div>
                    <div class="info">
                        <h4>Pending Books</h4>
                        <ul id="user_pending_books" style="margin-left: 20px;"></ul>
                    </div>`;

            var pending = document.getElementById('user_pending_books');
            pendingbooks.forEach(book => {
                pending.innerHTML += `<li>${book}</li>`;
            });
            if (pendingbooks.length == 0) {
                pending.innerHTML += `No pending books`;
            }


        }

    } catch (error) {
        displaydiv.innerHTML = `<div class="info">
                        <h4>Full Name</h4>
                        <p id="full-name">--------</p>
                    </div>
                    <div class="info">
                        <h4>Phone Number</h4>
                        <p id="user-phone">--------</p>
                    </div>
                    <div class="info">
                        <h4>Email</h4>
                        <p id="user-email">-----------</p>
                    </div>
                    <div class="info">
                        <h4>Join Date</h4>
                        <p id="user-join-date">----------</p>
                    </div>
                    <div class="info">
                        <h4>Pending Books</h4>
                       <ul id="user_pending_books" style="margin-left: 20px;"></ul>
                    </div>`;
        console.log("error in show user details");
        console.log(error.message);
    }

}
async function showLendRecordDetails(nic) {
    console.log(nic);

    if (!nic) {
        custoninfodiv.innerHTML = `<h3>Enter user Id to lode data</h3> `;
    }
    try {
        let infos = await getCustomInfo(nic);
        console.log(infos);

        if (infos.length == 0) {
            custoninfodiv.innerHTML = `No data found`;
        } else {
            custoninfodiv.innerHTML = ``;
            infos.forEach(rec => {
                custoninfodiv.innerHTML += ` <div>
                          <p>${rec.lentId}</p>
                          <p>${rec.bookTitle}</p>
                          <p>${rec.copies}</p>
                          <p>${rec.lentDate}</p>
                          <p>${rec.status}</p>
                        <button onclick="lodeinfo('${rec.lentId}', '${rec.copies}','${nic}','${rec.bookTitle}')">select</button>

                   </div>  </br>  `;
            });
        }
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
        return data;

    } catch (error) {
        console.error("Error fetching custom info:", error.message);
    }
}
function lodeinfo(lentid, copies,userid,title) {
    let count = document.getElementById('issue_bookcount');
    let Rtitle = document.getElementById('issue_title');
    count.value = copies;
    Rtitle.value = title;

    let receivebook = {
        "id": setid(),
        "usernic": Number(userid),
        "lentId": Number(lentid),
        "returncopies": Number( count.value)
    }

    let receiveBookButton = document.getElementById("recivebookbtn");

    // Remove any existing click event listeners on the button
    receiveBookButton.replaceWith(receiveBookButton.cloneNode(true));
    receiveBookButton = document.getElementById("recivebookbtn");

    // Add a new click event listener
    receiveBookButton.addEventListener('click', () => {
        console.log(receivebook);

    });
}
function setid() {
    return Number(Date.now().toString().substring(6));
};




showReturnrecods();



document.getElementById('issue_userid').addEventListener('input', () => {
    let id = document.getElementById('issue_userid').value;
    showUserDetails();

    showLendRecordDetails(id);
});














