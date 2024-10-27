
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
    try {
       var Book=  await fetchSingleISBN(isbn);
       console.log(Book);
    } catch (error) {
        
    }
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

function setid()
{
   return Number(Date.now().toString().substring(6));
};
showbooks_onAdminpage("true");
Add_dropdown_options();



document.getElementById('add-bookInventory').addEventListener('click', () => {
    document.getElementById('detail-container').style.display = 'none'; 
    document.getElementById('add-newbook-field').style.display = 'block';
});

document.getElementById('newbookcancel-btn').addEventListener('click', () => {
    document.getElementById('add-newbook-field').style.display = 'none'; 
    document.getElementById('detail-container').style.display = 'block'; 
});

document.getElementById('newbookadd-btn').addEventListener('click', async () => {
    const isbn = document.getElementById('isbn').value;
        const title = document.getElementById('title').value;
        const author = document.getElementById('author').value;
        const publishYear = document.getElementById('publishYear').value;
        const copies = document.getElementById('copies').value;
        const genre = document.getElementById('genre').value;

     await addNewBook(setid(),isbn,title,author,copies,publishYear,genre);

});

async function addNewBook(Id,isbn,title,author,copies,publishYear,genre) {
    const formData = new FormData();

    formData.append('Id', Id); 
    formData.append('ISBN', isbn);
    formData.append('Title',title );
    formData.append('Author', author);
    formData.append('Copies', copies);
    formData.append('PublishYear', publishYear);
    formData.append('Genre', genre);
    const imageInput = document.querySelector('#mimage'); 
    if (imageInput.files.length > 0) {
        for (let i = 0; i < imageInput.files.length; i++) {
            formData.append('Images', imageInput.files[i]);
        }
    }  

    try {
        const response = await fetch('https://localhost:7182/api/Book/AddNewBook', {
            method: 'POST',
            body: formData,
        });

        if (response.ok) {
            const data = await response.json();
            console.log('Book added successfully:', data);
        } else {
            const errorData = await response.json();
            console.error('Error adding book:', errorData);
        }
    } catch (error) {
        console.error('Network error:', error);
    }
}




async function fetchSingleISBN(isbn) {
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

async function editBook(isbn) {
    try {
        let uBook = await fetchSingleISBN(isbn);
        lodebookdetials(uBook);
        console.log(uBook);
        
    } catch (error) {
        console.log(error);
        
    }
}
function lodebookdetials(book) {
document.getElementById('newbookadd-btn').style.display = 'none';
document.getElementById('Update_book').style.display = 'block';

        document.getElementById('detail-container').style.display = 'none'; 
        document.getElementById('add-newbook-field').style.display = 'block';

             document.getElementById('isbn').value = book.isbn;
                document.getElementById('title').value = book.title;
                document.getElementById('author').value = book.author;
                document.getElementById('publishYear').value = book.publishYear;
                document.getElementById('copies').value = book.copies;
                document.getElementById('genre').value = book.genre;

}
async function updateBook(isbn, title, author, copies, publishYear, genre) {
    const formData = new FormData();
    formData.append('isbn', isbn);
    formData.append('Title', title);
    formData.append('Author', author);
    formData.append('Copies', copies);
    formData.append('PublishYear', publishYear);
    formData.append('Genre', genre);

    const imageInput = document.querySelector('#mimage');
    if (imageInput && imageInput.files.length > 0) {
        for (let i = 0; i < imageInput.files.length; i++) {
            formData.append('Images', imageInput.files[i]);
        }
    }

    try {
        const response = await fetch(`https://localhost:7182/api/Book/UpdateBook`, {
            method: 'PUT',
            body: formData,
        });

        if (response.ok) {
            const data = await response.json();
            console.log('Book updated successfully:', data);
        } else {
            const errorData = await response.json();
            console.error('Error updating book:', errorData);
        }
    } catch (error) {
        console.error('Network error:', error);
    }
}
document.getElementById('Update_book').addEventListener('click', async () => {
    const isbn = document.getElementById('isbn').value;
    const title = document.getElementById('title').value;
    const author = document.getElementById('author').value;
    const publishYear = document.getElementById('publishYear').value;
    const copies = document.getElementById('copies').value;
    const genre = document.getElementById('genre').value;


    await updateBook(isbn,title,author,copies,publishYear,genre);
});






