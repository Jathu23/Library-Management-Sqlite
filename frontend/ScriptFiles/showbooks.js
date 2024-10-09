async function fetchbooks() {
    try {
        let response = await fetch('https://localhost:7182/api/Book/GetAllBooks');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
        console.log(data);
        return data;
       
        

    } catch (error) {
        console.error('Error fetching books:', error);
        throw error; 
    }
}

async function showbooks_onAdminpage() {
    try {
        const books = await  fetchbooks();
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
        books.forEach(book => {
            table.innerHTML += `    <tbody>
      <td>${book.isbn}</td>
      <td>${book.title}</td>
      <td>${book.author}</td>
      <td>${book.publishYear}</td>
      <td>${book.addDateTime}</td>
      <td>${book.copies}</td>
      <td>${book.genre}</td>
      <td>${book.aviCopies}</td>
      <td><button>Edit</button> <button>Delete</button></td>
    </tbody>`;
          

            
        });
    } catch (error) {
        console.error('Error showing books:',error)
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


showbooks_onAdminpage();