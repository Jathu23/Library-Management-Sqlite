document.getElementById('issue_bookid').addEventListener('input', () => {
    console.log("jkdafgha");
    showBookDetails();
    let prea = document.getElementById('book-title').innerHTML;
    console.log(prea);
})



async function showBookDetails() {
    try {
        const isbn = document.getElementById('issue_bookid').value;

        if (isbn) {
            const bookData = await GetSingleBook(isbn);
            console.log(bookData.title);
            console.log(bookData.author);
            console.log(bookData.id);
            console.log(bookData.publishYear);
            let date = new Date().toLocaleDateString()
            console.log(date);

            // Get today's date
            let today = new Date();

            // Add 7 days to the current date
            today.setDate(today.getDate() + 7);

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
        console.log("lkdjhfjkdsgfsg");
        console.log(error.message);
    }

}



async function GetSingleBook(isbn) {
    try {
        let url = 'http://localhost:5102/api/Book/GetBook?isbn=' + encodeURIComponent(isbn);
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



// GetSingleBook("978-0-7432-7356-3");


document.getElementById('issue_userid').addEventListener('input', () => {
    console.log("jkdafgha");
    showUserDetails();
    let prea = document.getElementById('book-title').innerHTML;
    console.log(prea);
})


async function showUserDetails() {
    try {
        const NicNumber = document.getElementById('issue_userid').value;

        if (NicNumber) {
            const UserData = await GetSingleUser(NicNumber);
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







async function GetSingleUser(NicNumber) {
    try {
        let url = 'http://localhost:5102/api/User/GetByUser?nic=' + encodeURIComponent(NicNumber);
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
GetSingleUser(NicNumber)
