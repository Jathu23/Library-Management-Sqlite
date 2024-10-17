
const genreFilter = document.getElementById('genreFilter');
const authorFilter = document.getElementById('authorFilter');
const yearFilter = document.getElementById('yearFilter');
const bookTableBody = document.getElementById('bookTableBody');
let inputbar = document.getElementById('booksearchinput');



async function fetchbooks(ordervalue) {
    try {
        let url = 'https://localhost:7182/api/Book/GetOrderedByPublishYear?ascending=' + encodeURIComponent(ordervalue);
        let response = await fetch(url);
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
      
        return data;

    } catch (error) {
        console.error("Error fetching books:", error.message);
    }
 
}

async function fetchSearchBook(input) {
    try {
        let url = 'https://localhost:7182/api/Book/SearchByTitle?title=' + encodeURIComponent(input);
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

async function fetchpublishyears() {
    try {
        let response = await fetch('https://localhost:7182/api/Book/GetAllPublishYears');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
       
        return data;
    } catch (error) {
        console.error("An error occurred:", error.message);
    }
}
async function fetchGenres() {
    try {
        let response = await fetch('https://localhost:7182/api/Book/GetAllGenres');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
       
        return data;
    } catch (error) {
        console.error("An error occurred:", error.message);
    }
}



async function fetchAuthors() {
    try {
        let response = await fetch('https://localhost:7182/api/Book/GetAllAuthors');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
       
        return data;
    } catch (error) {
        console.error("An error occurred:", error.message);
    }
}
async function fetchCategorizebooks(genre, author, publishYear) {

    let url = 'https://localhost:7182/api/Book/Categorization?';


    
    if (genre) {
        url += `genre=${encodeURIComponent(genre)}&`;
        console.log(genre);
        
    }
    if (author) {
        url += `author=${encodeURIComponent(author)}&`;
        console.log(author);
        
    }
    if (publishYear) {
        url += `publishYear=${encodeURIComponent(publishYear)}&`;
        console.log(publishYear);
        
    }

    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error(`Error: ${response.status} - ${response.statusText}`);
        }

        const books = await response.json();

        return books;

    } catch (error) {
        console.error('Error fetching books:', error);
    }
}
async function showbooks_onAdminpage(value) {
    try {
        const books = await  fetchbooks(value);
        await displaybooks(books);
  
    } catch (error) {
        console.error('Error showing books:',error)
    }
}



async function deleteBook_api(isbn) {
    try {
        let response = await fetch(`https://localhost:7182/api/Book/DeleteBook?id=` + encodeURIComponent(isbn), {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        let result = await response.json();
        console.log("Book deleted successfully:", result);
        alert("Book deleted successfully");
    } catch (error) {
        console.error("An error occurred while deleting the book:", error);
        alert("Failed to delete book: " + error.message);
    }
}
async function displaybooks(booksarray) {
    let table = document.getElementById('bookinv_table');
    table.innerHTML = ` <thead>
      <tr>
        <td>ISBN</td>
        <td>Title</td>
        <td>Author</td>
        <td>Publish Year</td>
        <td>Add Date</td>
        <td>Copies</td>
        <td>Genre</td>
        <td>Av.Copies</td>
        <td>Actions</td>   
      </tr>
    </thead>`;
    booksarray.forEach(book => {
        table.innerHTML += `    <tbody>
  <td>${book.isbn}</td>
  <td>${book.title}</td>
  <td>${book.author}</td>
  <td>${book.publishYear}</td>
  <td>${new Date(book.addDateTime).toLocaleDateString()}</td>
  <td>${book.copies}</td>
  <td>${book.genre}</td>
  <td>${book.aviCopies}</td>
  <td>
  <button onclick="editBook('${book.isbn}')">Edit</button>
  <button onclick="deleteBook('${book.isbn}')">Delete</button></td>
</tbody>`;

        
    });
}
async function showsearchbooks(input) {
    try {
        let books = await fetchSearchBook(input);
        await displaybooks(books);
    } catch (error) {
        
    }
}
async function showbooks_onUserpage() {
    try {
        const books = await  fetchbooks();
       
        books.forEach(book => {
            console.log(book.title);
            
        });
    } catch (error) {
        console.error('Error showing books:',error)
    }
}
async function Add_dropdown_options() {
try {
    const genres = await fetchGenres();
    const authors = await fetchAuthors();
    const years = await fetchpublishyears();
  
    createDropdown(genreFilter, genres);
    createDropdown(authorFilter, authors);
    createDropdown(yearFilter, years);
} catch (error) {
    
}

}


function createDropdown(dropdown, options) {
  options.forEach(option => {
    const opt = document.createElement('option');
    opt.value = option;
    opt.textContent = option;
    dropdown.appendChild(opt);
  });
}
async function filterbooks() {
  let selectGener =genreFilter.value;
  let selectAuthor =authorFilter.value;
  let selectYear =yearFilter.value;

  try {
    let books = await fetchCategorizebooks(selectGener,selectAuthor,selectYear);
   await displaybooks(books);

  } catch (error) {
    console.log(error.message);
  }

}
async function deleteBook(isbn) {
   await deleteBook_api(isbn);
   showbooks_onAdminpage("true");
}

async function editBook(isbn) {
    alert(isbn);
    showbooks_onAdminpage("false");
}




inputbar.addEventListener('input', () => {
    let input = inputbar.value;
    if (!input) {
        showbooks_onAdminpage("true");
    }else{
        showsearchbooks(inputbar.value);
    }
   
});

genreFilter.addEventListener('change',() =>{
    filterbooks();
});
authorFilter.addEventListener('change',() =>{
    filterbooks();
});
yearFilter.addEventListener('change',() =>{
    filterbooks();
});

showbooks_onAdminpage("true");
Add_dropdown_options();













 // Toggle between inventory and add book form
 document.getElementById('add-bookInventory').addEventListener('click', () => {
    document.getElementById('detail-container').style.display = 'none'; // Hide inventory
    document.getElementById('add-newbook-field').style.display = 'block'; // Show add book form
  });

  // Cancel and go back to the inventory view
  document.getElementById('newbookcancel-btn').addEventListener('click', () => {
    document.getElementById('add-newbook-field').style.display = 'none'; // Hide add book form
    document.getElementById('detail-container').style.display = 'block'; // Show inventory
  });

  // Add book to the server when "Add Book" is clicked
  document.getElementById('newbookadd-btn').addEventListener('click', async () => {
    // Get form values
    const isbn = document.getElementById('isbn').value;
    const title = document.getElementById('title').value;
    const author = document.getElementById('author').value;
    const publishYear = document.getElementById('publishYear').value;
    const copies = document.getElementById('copies').value;
    const genre = document.getElementById('genre').value;
    const image = document.getElementById('mimage').files[0];

    // Basic validation
    if (!isbn || !title || !author || !publishYear || !copies || !genre || !image) {
      alert("Please fill in all fields.");
      return;
    }

    // Generate a unique ID
    const id = Number(Date.now().toString().substring(7));
    console.log("Generated ID:", id);

    alert("Attempting to add the book...");
    console.log("Adding book with details:", { isbn, title, author, publishYear, copies, genre });

    // Call the function to add the new book
    await AddNewBook(id, isbn, title, author, publishYear, copies, genre, image);
  });

  // Function to send book details to the server
  async function AddNewBook(id, isbn, title, author, publishYear, copies, genre, image) {
    try {
      const url = 'https://localhost:7182/api/Book/AddNewBook';
      
      // Create FormData object to handle file and text data
      const formData = new FormData();
      formData.append('id', id);
      formData.append('isbn', isbn);
      formData.append('title', title);
      formData.append('author', author);
      formData.append('publishYear', publishYear);
      formData.append('copies', copies);
      formData.append('genre', genre);
      formData.append('image', image);
console.log(formData);

      // Send a POST request
      const response = await fetch(url, {
        method: 'POST',
        body: formData // Send the FormData directly
      });

      // Check if the response is OK
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }

      // Parse the response JSON
      const result = await response.json();
      console.log("Book added successfully:", result);
      alert("Book added successfully!");

      // Reset the form after successful submission
      document.getElementById('isbn').value = '';
      document.getElementById('title').value = '';
      document.getElementById('author').value = '';
      document.getElementById('publishYear').value = '';
      document.getElementById('copies').value = '';
      document.getElementById('genre').value = '';
      document.getElementById('mimage').value = '';

      // Switch back to the inventory view
      document.getElementById('add-newbook-field').style.display = 'none'; // Hide add book form
    document.getElementById('detail-container').style.display = 'block'; 

    } catch (error) {
      console.error("An error occurred while adding the book:", error.message);
      alert("Failed to add the book.");
    }
  }