async function fetchreturnrecods() {
    try {
        let response = await fetch('https://localhost:7182/api/Return/GetAllRecods');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
       console.log(data);
        return data;
    } catch (error) {
        console.error("An error occurred:", error.message);
    }
}
fetchreturnrecods();