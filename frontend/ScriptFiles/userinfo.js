let uName = document.getElementById('memberName');
let profilepic = document.getElementById('profileImage');
let profilepic2 = document.getElementById('profileImage2');
let usernic =JSON.parse(localStorage.getItem("LoginNic"));
console.log(usernic);
async function getUserByNIC(nic) {
    try {
        const url = `https://localhost:7182/api/User/GetByUser?nic=${nic}`;
        
        let response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        let data = await response.json();
 return data

    } catch (error) {
        console.error("Error fetching user details:", error.message);
    }
}

async function displayuserinfo(nicnum) {
    try {
        let curentUser = await getUserByNIC(nicnum);
        uName.innerText= curentUser.fullName;
        profilepic.src ='../member/defaultuserimg.png';
        profilepic2.src ='../member/defaultuserimg.png';

    
    } catch (error) {
        console.error("Error fetching user details:", error.message);
    }  
}

displayuserinfo(usernic);





