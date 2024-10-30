document.addEventListener('DOMContentLoaded', () => {
    let usernic =JSON.parse(localStorage.getItem("LoginNic"));
    
    async function fetchReturnRecordsByUserNIC(usernic) {
        try {
            const response = await fetch(`https://localhost:7182/api/History/ReturnByUserNIC?usernic=${usernic}`);
            
            if (!response.ok) {
                throw new Error(`Error: ${response.statusText}`);
            }
            let data = await response.json();
            console.log(data);
            
            return data;
        } catch (error) {
            console.error("Failed to fetch return records:", error);
            return [];
        }
    }
    async function fetchLentHistoryByUserNIC(usernic) {
        try {
            const response = await fetch(`https://localhost:7182/api/History/LentByUserNIC?usernic=${usernic}`);
            
            if (!response.ok) {
                throw new Error(`Error: ${response.statusText}`);
            }
    
            return await response.json();
        } catch (error) {
            console.error("Failed to fetch lent history:", error);
            return [];
        }
    }
    
    
    function displayReturnRecords(returnBookDetails) {
        const tableBody = document.getElementById("returnRecordsTable").querySelector("tbody");
        
       
        tableBody.innerHTML = "";
    
     
        returnBookDetails.forEach(record => {
            const row = document.createElement("tr");
    
            row.innerHTML = `
                <td>${record.id}</td>
                <td>${record.isbn}</td>
                <td>${record.title}</td>
                <td>${record.lentId}</td>
                <td>${record.lentCopies}</td>
                <td>${record.returnCopies}</td>
                <td>${new Date(record.returnDate).toLocaleDateString()}</td>
            `;
            
            tableBody.appendChild(row);
        });
    }
    function displayLentHistory(lentHistory) {
        const tableBody = document.getElementById("lentHistoryTable").querySelector("tbody");
        tableBody.innerHTML = "";
    
        lentHistory.forEach(record => {
            const row = document.createElement("tr");
            row.innerHTML = `
                <td>${record.id}</td>
                <td>${record.isbn}</td>
                <td>${record.title}</td>
                <td>${new Date(record.lentDate).toLocaleDateString()}</td>
                <td>${new Date(record.returnDate).toLocaleDateString()}</td>
                <td>${record.copies}</td>
            `;
            tableBody.appendChild(row);
        });
    }
  
    async function loadReturnRecords(usernic) {
        if (usernic) {
            const returnBookDetails = await fetchReturnRecordsByUserNIC(usernic);
            displayReturnRecords(returnBookDetails);
        } else {
            alert("Please enter a valid UserNIC.");
        }
    }
    async function loadLentRecords(usernic) {
        if (usernic) {
            const lentHistory = await fetchLentHistoryByUserNIC(usernic);
        displayLentHistory(lentHistory);
        } else {
            alert("Please enter a valid UserNIC.");
        }
    }


    loadReturnRecords(usernic);
    loadLentRecords(usernic);




    async function fetchOverdueLentRecords(usernic) {
        try {
            const response = await fetch(`https://localhost:7182/api/Lent/findoverdue_user?nic=${usernic}`);
            
            if (!response.ok) {
                throw new Error(`Error: ${response.statusText}`);
            }
    
            return await response.json();
        } catch (error) {
            console.error("Failed to fetch overdue lent records:", error);
            return [];
        }
    }
    
    function displayOverdueLentRecords(overdueRecords) {
        const tableBody = document.getElementById("overdueLentRecordsTable").querySelector("tbody");
        tableBody.innerHTML = "";
    
        overdueRecords.forEach(record => {
            const row = document.createElement("tr");
            row.innerHTML = `
                <td>${record.lentid}</td>
                <td>${record.isbn}</td>
                <td>${record.copies}</td>
                <td>${new Date(record.lentDate).toLocaleDateString()}</td>
                <td>${new Date(record.returnDate).toLocaleDateString()}</td>
                <td>${record.status}</td>
            `;
            tableBody.appendChild(row);
        });
    }
    
   
    async function loadOverdueRecords(usernic) {
        if (usernic) {
            const overdueRecords = await fetchOverdueLentRecords(usernic);
            displayOverdueLentRecords(overdueRecords);
        } else {
            console.log ("Please enter a valid UserNIC.");
           
        }
    }
    
    loadOverdueRecords(usernic);
});