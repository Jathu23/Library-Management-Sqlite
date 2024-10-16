
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
        <td>Available Copies</td>
        <td>Actions</td>   
      </tr>
    </thead>`;
    booksarray.forEach(book => {
        table.innerHTML += `    <tbody>
  <td>${book.isbn}</td>
  <td>${book.title}</td>
  <td>${book.author}</td>
  <td>${book.publishYear}</td>
  <td>${book.addDateTime}</td>
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
