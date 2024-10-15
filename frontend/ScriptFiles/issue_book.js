document.getElementById('issue_bookid').addEventListener('input', () => {
    showBookDetails();
})


document.getElementById('issue_userid').addEventListener('input', () => {
    showUserDetails();
})

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


async function showBookDetails() {
    try {
        const isbn = document.getElementById('issue_bookid').value;

        if (true) {
            const bookData = await fetchSingleBook(isbn);
       
            let date = new Date().toLocaleDateString()
            console.log(date);

            // Get today's date
            let today = new Date();

            // Add 7 days to the current date
            today.setDate(today.getDate() + 14);

            // Format the new date to a readable string (optional)
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











































document.getElementById('issue-book-btn').addEventListener('click', async () => {
    // Get the latest values when the button is clicked
    const lentCopies = document.getElementById("issue_bookcount").value;
    const NicNumber = document.getElementById('issue_userid').value;
    const isbn = document.getElementById('issue_bookid').value;

    // Optional: Basic validation to check if fields are filled
    if (!isbn || !NicNumber || !lentCopies) {
        alert("Please fill in all fields.");
        return;
    }

    // Call the function to add the lent book
    alert("Attempting to lend the book...");
    console.log("Lending book with details:", { isbn, NicNumber, lentCopies });
    await AddlendBook(isbn, NicNumber, lentCopies);
});

async function AddlendBook(isbn, NicNumber, lentCopies) {
    try {
        // Construct the URL for the API endpoint
        const url = 'http://localhost:7182/api/Lent/Add';
        
        // Create the data object to send
        const data = {
            isbn: isbn,
            usernic: NicNumber,
            copies: lentCopies
        };

        // Send a POST request
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json' // Specify that we're sending JSON
            },
            body: JSON.stringify(data) // Convert the data object to a JSON string
        });

        // Check if the response is OK
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        // Parse the response JSON
        const result = await response.json();
        console.log("Book lent successfully:", result);
        return result; // Return the result if needed

    } catch (error) {
        console.error("An error occurred while lending the book:", error.message);
    }
}


