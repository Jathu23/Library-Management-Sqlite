async function ValidateUser(nic, password) {
    try {
        let url = 'https://localhost:7182/api/User/ValidateUser?nic=' + encodeURIComponent(nic) + '&password=' + encodeURIComponent(password);

        let response = await fetch(url);

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        let data = await response.json();
console.log(data);

        return data;
        
    } catch (error) {
        console.error("Error validating user:", error.message);
    }
}



document.getElementById("login").addEventListener("click", function() {
    document.getElementById("logincontainer").style.display = "none";
    document.getElementById("user-creation-page").style.display = "block";
});

document.getElementById("signup").addEventListener("click", function() {
    document.getElementById("user-creation-page").style.display = "none";
    document.getElementById("logincontainer").style.display = "block";
});

document.getElementById("loginForm").addEventListener("submit", async function (event) {
    event.preventDefault(); 

    const nicNumber = document.getElementById("loginNIC").value;
    const password = document.getElementById("loginPassword").value;
    const Urole = document.getElementById("LoginuserRole").value;
try {
    if (Urole == "member") {
        let validate = await ValidateUser(nicNumber,password);
        if (validate) {
            
            window.location.replace("../member/member.html?nicnumber=" + nicNumber);
          
        }else{
            document.getElementById("loginMessage").textContent = "User Id or Password incorrect";

            setTimeout(() => {
                document.getElementById("loginMessage").textContent = "";
            }, 2000);
        }
    }else{
        if (nicNumber == "admin" && password == "admin" ) {
            window.location.replace("../new-admin/dashboard-Admin .html");
        }else{
            document.getElementById("loginMessage").textContent = "User Id or Password incorrect";
            setTimeout(() => {
                document.getElementById("loginMessage").textContent = "";
            }, 2000);
        }
    }
 
} catch (error) {
    console.error('There was an error with the login:', error);
    document.getElementById("loginMessage").textContent = "An error occurred. Please try again.";
    setTimeout(() => {
        document.getElementById("loginMessage").textContent = "";
    }, 3000);
}


});






