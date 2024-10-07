
        document.getElementById("loginForm").addEventListener("submit", async function (event) {
            event.preventDefault(); // Prevent default form submission
        
            // Get form values and trim whitespace
            const nicNumber = document.getElementById("nicnumber").value.trim();
            const loginPassword = document.getElementById("loginPassword").value.trim();
            const userRole = document.getElementById("userRole").value;
        
            // Validate inputs
            if (!nicNumber || !loginPassword) {
                document.getElementById("loginMessage").innerText = "Please fill in all fields.";
                return;
            }
        
            try {
                // Fetch users from JSON server
                const response = await fetch("http://localhost:3000/users");
                if (!response.ok) {
                    throw new Error("Failed to fetch users.");
                }
        
                const users = await response.json();
        console.log(users)
                // Check for matching user credentials
                const user = users.find(user => 
                    user.NICNumber === nicNumber && user.Password === loginPassword && user.UserRole === userRole
                );
        
                if (user) {
                    // Store user details in local storage
                    localStorage.setItem("loggedInUser", JSON.stringify(user));
        
                    // Redirect based on user role
                    if (userRole === "member") {
                        window.location.href = "../member/member.html"; // Redirect to member page
                    } else if (userRole === "admin") {
                        window.location.href ="../new-admin/dashboard-Admin .html"; // Redirect to admin page
                    }
                } else {
                    document.getElementById("loginMessage").innerText = "Invalid login details. Please try again.";
                }
            } catch (error) {
                document.getElementById("loginMessage").innerText = "Network error. Please try again later.";
                console.error(error);
            }
        });
       