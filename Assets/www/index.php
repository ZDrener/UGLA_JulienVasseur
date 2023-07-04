<?php

// Database connection details
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "ugla";

// Create a connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check the connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Fetch series data from the database
$sql = "SELECT id, title, genre, note, episodes FROM series";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // Create an array to store series data
    $series = array();

    while ($row = $result->fetch_assoc()) {
        // Add series data to the array
        $series[] = $row;
    }

    // Convert the array to JSON
    $json = json_encode($series, JSON_PRETTY_PRINT);

    // Save the JSON data to a file
    file_put_contents('series.json', $json);

    echo "JSON file generated successfully.";
} else {
    echo "No series data found.";
}

// Close the database connection
$conn->close();

?>