let cads = document.getElementById("cardBox");
async function fetchBookCount() {
    try {
        let response = await fetch('https://localhost:7182/api/Book/CountTotalBooks');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching book count:', error);
        throw error; 
    }
}

async function fetchMemberCount() {
    try {
        let response = await fetch('https://localhost:7182/api/User/CountTotalUsers');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
       
        return data;
    } catch (error) {
        console.error('Error fetching users count:', error);
        throw error; 
    }
}

async function fetchLendBooks() {
    try {
        let response = await fetch('https://localhost:7182/api/Lent/CountTotalLendBooks');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
        
        return data;
    } catch (error) {
        console.error('Error fetching users count:', error);
        throw error; 
    }
}


async function fetchReturnBooks() {
    try {
        let response = await fetch('https://localhost:7182/api/Lent/findoverdueAll');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
      
        return data;
    } catch (error) {
        console.error('Error fetching users count:', error);
        throw error; 
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
async function fetchGatAlluser() {
    try {
        let response = await fetch('https://localhost:7182/api/Book/GetAllGenres');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
       
        return data;
    } catch (error) {
        console.error('Error fetching users count:', error);
        throw error; 
    }
}


async function fetchGatAuthor() {
    try {
        let response = await fetch('https://localhost:7182/api/Book/GetAllAuthors');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
       
        return data;
    } catch (error) {
        console.error('Error fetching users count:', error);
        throw error; 
    }
}

async function fetchTotalLendTimes() {
    try {
        let response = await fetch('https://localhost:7182/api/History/Lent');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
        
        return data;
    } catch (error) {
        console.error('Error fetching users count:', error);
        throw error; 
    }
}

async function getPopularBooks(limit) {
    try {
        const response = await fetch(`https://localhost:7182/api/Book/GetPopularBooks?limit=${limit}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const popularBooks = await response.json();
       return popularBooks;
    } catch (error) {
        console.error('Error fetching popular books:', error);
    }
}







async function updateDashboard() {
    try {
        const count = await fetchBookCount(); 
        const usercount= await fetchMemberCount();
        const lendcounts =await fetchLendBooks();
        const returncounts =await fetchReturnBooks();
        const notreturncounts = returncounts.length;
        const genre =await  fetchGenres();
        const counts =  genre.length; 
        const GatAuthor =await fetchGatAuthor();
        const counts1 =  GatAuthor.length; 
        const lendtimes = await fetchTotalLendTimes();
        const lendtimes1 = lendtimes.length;
        const pbook = await getPopularBooks(1);


        
        cads.children[1].children[0].children[0].innerHTML = count; 
        cads.children[0].children[0].children[0].innerHTML =usercount;
        cads.children[2].children[0].children[0].innerHTML=lendcounts;
        cads.children[3].children[0].children[0].innerHTML=notreturncounts;
        cads.children[4].children[0].children[0].innerHTML=counts;
        cads.children[5].children[0].children[0].innerHTML=counts1;
        cads.children[6].children[0].children[0].innerHTML=lendtimes1;

        document.getElementById('popularbook').innerHTML =`  <p>${pbook[0].title}</P> <hr>
              <p>Author : ${pbook[0].author}</p>
              <p>Genre : ${pbook[0].genre}</p>
              <p>${pbook[0].aviCopies} Copies Aviable </p>
              <p>Rented: ${pbook[0].rentCount} Times </p>`;
      
    } catch (error) {
        console.error('Error updating dashboard:', error); 
    }
}

updateDashboard();


