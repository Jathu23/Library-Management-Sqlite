let cads = document.getElementById("cardBox");
console.log( cads.children[1].children[0].children[0].innerHTML );
console.log( cads.children[0].children[0].children[0].innerHTML );
console.log( cads.children[2].children[0].children[0].innerHTML );

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
        console.log(data);
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
        console.log(data);
        return data;
    } catch (error) {
        console.error('Error fetching users count:', error);
        throw error; 
    }
}

async function updateDashboard() {
    try {
        const count = await fetchBookCount(); 
        const usercount= await fetchMemberCount();
        const lendcounts =await fetchLendBooks();
        cads.children[1].children[0].children[0].innerHTML = count; 
        cads.children[0].children[0].children[0].innerHTML =usercount;
        cads.children[2].children[0].children[0].innerHTML=lendcounts;
    } catch (error) {
        console.error('Error updating dashboard:', error); 
    }
}

updateDashboard();


