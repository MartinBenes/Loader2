<?php
$ini = parse_ini_file('config.ini');
$link = mysqli_connect($ini['db_host'],$ini['db_user'],$ini['db_password']);
$database = mysqli_select_db($link,$ini['db_name']);

$user = $_GET['username'];
$password = $_GET['password'];
$tables = $ini['mybb_usertable'];

$sql = "SELECT * FROM ". $tables ." WHERE username = '". mysqli_real_escape_string($link,$user) ."'" ;
$result = $link->query($sql);

if ($result->num_rows > 0) {
	// Outputting the rows
	while($row = $result->fetch_assoc())
	{
		
		$password = $row['password'];
		$salt = $row['salt'];
		$plain_pass = $_GET['password'];
		$stored_pass = md5(md5($salt).md5($plain_pass));
		
		function Redirect($url, $permanent = false)
		{
			if (headers_sent() === false)
			{
				header('Location: ' . $url, true, ($permanent === true) ? 301 : 302);
			}
		exit();
		}
		
		if($stored_pass != $row['password'])
		{
			echo "0"; // Wrong pass, user exists
		}
		else
		{
			echo "1"; // Correct pass
		}
	}
} 
else
{
	echo "2"; // User doesn't exist
}


//-----------------------------------------//
//          Thaisen's Loader v2.0          //
//										   //
//   Copyright © Tyson Prefontaine-McRae   //
// 										   //
// Removing these lines breaks the license //
//-----------------------------------------//
?>

<head>
<title>Checking login info</title>
</head>