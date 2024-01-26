document.addEventListener("DOMContentLoaded", function () {
    // Get references to the date input and age input
    var dateOfBirthInput = document.getElementById('dateOfBirth');
    var ageInput = document.getElementById('age');

    // Add a change event listener to the date input
    dateOfBirthInput.addEventListener('change', function () {
        // Get the selected date
        var selectedDate = new Date(dateOfBirthInput.value);

        // Calculate the age
        var today = new Date();
        var age = today.getFullYear() - selectedDate.getFullYear();

        // Adjust age if the birthday hasn't occurred yet this year
        if (today.getMonth() < selectedDate.getMonth() || (today.getMonth() === selectedDate.getMonth() && today.getDate() < selectedDate.getDate())) {
            age--;
        }

        // Update the age input value
        ageInput.value = age;
    });
    // Function to calculate age based on date of birth
    function calculateAge(selectedDate) {
        var today = new Date();
        var age = today.getFullYear() - selectedDate.getFullYear();

        if (today.getMonth() < selectedDate.getMonth() || (today.getMonth() === selectedDate.getMonth() && today.getDate() < selectedDate.getDate())) {
            age--;
        }

        return age;
    }

    // Function to update the age field
    function updateAge() {
        var selectedDate = new Date(dateOfBirthInput.value);
        var age = calculateAge(selectedDate);
        ageInput.value = age;
    }

    // Add a change event listener to the date input
    dateOfBirthInput.addEventListener('change', updateAge);

    // Initialize age when the page loads
    updateAge();
});