let addBtn = document.getElementById("add-bookInventory");
let newBookBtn = document.getElementById("add-newbook-field");
let saveBtn = document.getElementById("newbookadd-btn");
let cancelBtn = document.getElementById("newbookcancel-btn");
let searchInput = document.querySelector(".search input");
let bookInventoryField = document.getElementById("book-inventory-field");

// Show the form to add a new book
addBtn.addEventListener("click", function(event) {
    event.preventDefault();
    showAddBookForm();//210

});

// Hide the form and clear input fields when canceling
cancelBtn.addEventListener("click", function(event) {
    event.preventDefault();
    cancelAddBook();
});

// Function to generate a new book ID automatically
function generateBookID() {
    let books = JSON.parse(localStorage.getItem('books')) || [];
    return `BN${books.length + 100}`; // ID based on the number of books
}

// Function to get book details from input fields and add them to the table
function addBookToTable() {
    // Retrieve input values
    const bookID = document.getElementById('bookid').value;
    const bookName = document.getElementById('bookname').value;
    const author = document.getElementById('author').value;
    const category = document.getElementById('Category').value; // Corrected ID
    const publishDate = document.getElementById('bookpublishdate').value;
    const totalCopies = document.getElementById('totalcoppies').value;

    // Validate inputs
    if (!bookName || !author || !category || !publishDate || !totalCopies) {
        alert("Please fill in all fields.");
        return;
    }

    // Add the new book to the table
    const table = document.querySelector('table tbody');
    const newRow = document.createElement('tr');

    newRow.innerHTML = `
        <td>${bookID}</td>
        <td>${bookName}</td>
        <td>${author}</td>
        <td>${publishDate}</td>
        <td>${category}</td>
        <td>${totalCopies}</td>
        <td><span class="status available">${totalCopies}</span></td>
        <td><ion-icon name="create-outline" class="edit-icon"></ion-icon></td>
        <td><ion-icon name="trash-outline" class="delete-icon"></ion-icon></td>
    `;

    // Append the new row to the table
    table.appendChild(newRow);

    // Store the new book in local storage
    let books = JSON.parse(localStorage.getItem('books')) || [];
    books.push({
       bookId: bookID,
        bookname: bookName,
        author: author,
        category: category,
        publishDate: publishDate,
        totalCopies: totalCopies,
        availableCopies: totalCopies
    });
    localStorage.setItem('books', JSON.stringify(books));

    // Add delete and edit event listeners to the new row
    addRowEventListeners(newRow);

    // Clear the input fields
    clearInputFields();

    // Hide the add book form
    newBookBtn.style.display = 'none';

    // Provide user feedback
    alert("Book added successfully!");
    bookInventoryField.style.display="block";
}

// Function to add delete and edit event listeners to a row
function addRowEventListeners(row) {
    const deleteIcon = row.querySelector('.delete-icon');
    deleteIcon.addEventListener('click', function() {
        deleteBook(row.querySelector('td').innerText, row); // Pass the book ID from the first cell
    });

    const editIcon = row.querySelector('.edit-icon');
    editIcon.addEventListener('click', function() {
        editBook(row.querySelector('td').innerText, row); // Pass the book ID from the first cell
    });
}

// Function to load books from local storage and populate the table
function loadBooksFromLocalStorage() {
    let books = JSON.parse(localStorage.getItem('books')) || [];
    const table = document.querySelector('table tbody');
    
    books.forEach(book => {
        const newRow = document.createElement('tr');
        newRow.innerHTML = `
            <td>${book.bookId}</td>
            <td>${book.bookname}</td>
            <td>${book.author}</td>
            <td>${book.publicationDate}</td>
            <td>${book.category}</td>
            <td>${book.totalCopies}</td>
            <td><span class="status available">${book.availableCopies}</span></td>
            <td><ion-icon name="create-outline" class="edit-icon"></ion-icon></td>
            <td><ion-icon name="trash-outline" class="delete-icon"></ion-icon></td>
        `;
        table.appendChild(newRow);

        // Add delete and edit event listeners to the new row
        addRowEventListeners(newRow);
    });
}

// Function to delete a book from the table and local storage
function deleteBook(bookID, rowElement) {
    // Remove the row from the table
    rowElement.remove();

    // Debugging: Log the bookID and the books array
    console.log("Deleting book with ID:", bookID);

    // Remove the book from local storage
    let books = JSON.parse(localStorage.getItem('books')) || [];
    
    books = books.filter(book => {
        console.log("Comparing with:", book.bookId); // Check comparison
        return book.bookId !== bookID;
    });

    localStorage.setItem('books', JSON.stringify(books));

    // Provide user feedback
    alert("Book deleted successfully!");
}


// Function to edit a book in the table and local storage
function editBook(bookID, rowElement) {
    // Get the current book details
    const cells = rowElement.querySelectorAll('td');
    const bookName = cells[1].innerText;
    const author = cells[2].innerText;
    const publishDate = cells[3].innerText;
    const category = cells[4].innerText;
    const totalCopies = cells[5].innerText;

    // Populate the input fields with the current details
    document.getElementById('bookid').value = bookID;
    document.getElementById('bookname').value = bookName;
    document.getElementById('author').value = author;
    document.getElementById('Category').value = category; // Corrected ID
    document.getElementById('bookpublishdate').value = publishDate;
    document.getElementById('totalcoppies').value = totalCopies;

    // Show the add book form
    newBookBtn.style.display = 'block';

    // Update the add button to save the changes instead of adding a new book
    saveBtn.removeEventListener('click', addBookToTable);
    saveBtn.addEventListener('click', function updateBook() {
        // Update the book details in the table
        cells[1].innerText = document.getElementById('bookname').value;
        cells[2].innerText = document.getElementById('author').value;
        cells[3].innerText = document.getElementById('bookpublishdate').value;
        cells[4].innerText = document.getElementById('Category').value; // Corrected ID
        cells[5].innerText = document.getElementById('totalcoppies').value;
        cells[6].querySelector('.status').innerText = document.getElementById('totalcoppies').value;

        // Update the book in local storage
        let books = JSON.parse(localStorage.getItem('books')) || [];
        books = books.map(book => {
            if (book.bookId === bookID) {
                return {
                    bookId: bookID,
                    name: cells[1].innerText,
                    author: cells[2].innerText,
                    category: cells[4].innerText,
                    publishDate: cells[3].innerText,
                    totalCopies: cells[5].innerText,
                    availableCopies: cells[5].innerText // assuming available copies is the same as total copies
                };
            }
            return book;
        });
        localStorage.setItem('books', JSON.stringify(books));

        // Clear the input fields
        clearInputFields();

        // Hide the add book form
        newBookBtn.style.display = 'none';

        // Provide user feedback
        alert("Book updated successfully!");

        // Reset the save button event listener to add new books
        saveBtn.removeEventListener('click', updateBook);
        saveBtn.addEventListener('click', addBookToTable);
    });
}
// Function to cancel adding a new book
function cancelAddBook() {
    clearInputFields();
    newBookBtn.style.display = 'none';
 bookInventoryField.style.display="block";
}
// Function to show the "Add New Book" form and set the generated ID
function showAddBookForm() {
    const newBookID = generateBookID();
    document.getElementById('bookid').value = newBookID; 
    newBookBtn.style.display = 'block';
    bookInventoryField.style.display="none";
}

// Function to search and filter books in the table
function searchBooks() {
    const filter = searchInput.value.toLowerCase();
    const rows = document.querySelectorAll('table tbody tr');

    rows.forEach(row => {
        const cells = row.querySelectorAll('td');
        const bookDetails = `${cells[0].innerText} ${cells[1].innerText} ${cells[2].innerText} ${cells[3].innerText} ${cells[4].innerText}`.toLowerCase();

        if (bookDetails.includes(filter)) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}

// Function to clear input fields
function clearInputFields() {
    document.getElementById('bookid').value = '';
    document.getElementById('bookname').value = '';
    document.getElementById('author').value = '';
    document.getElementById('Category').value = ''; // Corrected ID
    document.getElementById('bookpublishdate').value = '';
    document.getElementById('totalcoppies').value = '';
}

// Event listeners for the buttons
saveBtn.addEventListener('click', addBookToTable);
cancelBtn.addEventListener('click', cancelAddBook);
addBtn.addEventListener('click', showAddBookForm);
searchInput.addEventListener('input', searchBooks);

// Load books from local storage on page load
window.addEventListener('load', loadBooksFromLocalStorage);
