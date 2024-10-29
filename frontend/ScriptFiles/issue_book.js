const deuration = 7;

document.getElementById('issue_bookid').addEventListener('input', () => {
    showBookDetails();
})


document.getElementById('issue_userid').addEventListener('input', () => {
    showUserDetails();
})
document.getElementById('issue_date').value = new Date().toISOString().split('T')[0];


async function fetchSingleBook(isbn) {
    try {
        let url = 'https://localhost:7182/api/Book/GetBook?isbn=' + encodeURIComponent(isbn);
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


async function showBookDetails() {
    try {
        const isbn = document.getElementById('issue_bookid').value;

        if (true) {
            const bookData = await fetchSingleBook(isbn);

            
          

            
            let today = new Date();

            
            today.setDate(today.getDate() + deuration);

           
            let futureDate = today.toLocaleDateString();




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
                        <p id="user-join-date">${new Date(UserData.joinDate).toLocaleDateString() }</p>
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
        console.log("error in show user details");
        console.log(error.message);
    }

}


document.getElementById('issue-book-btn').addEventListener('click', async () => {

    const copies = document.getElementById("issue_bookcount").value;
    const NicNumber = document.getElementById('issue_userid').value;
    const isbn = document.getElementById('issue_bookid').value;
  

    if (!isbn || !NicNumber || !copies) {
        alert("Please fill in all fields.");
        return;
    }


    const id = Number(Date.now().toString().substring(7));
    console.log("Generated ID:", id);

    alert("Attempting to lend the book...");
    console.log("Lending book with details:", { isbn, NicNumber, copies });
    await AddlendBook(id, isbn, NicNumber, Number(copies));
    document.getElementById("issue-bookform").reset();
    document.getElementById('issue_date').value = new Date().toISOString().split('T')[0];
});

async function AddlendBook(id, isbn, NicNumber, copies) {
    try {

        const url = 'https://localhost:7182/api/Lent/Add';


        const data = {
            id: id,
            isbn: isbn,
            usernic: NicNumber,
            lentcopies: copies
        };


        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });


        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }


        const result = await response.json();
        console.log("Book lent successfully:", result);
        return result;

    } catch (error) {
        console.error("An error occurred while lending the book:", error.message);
    }
}
