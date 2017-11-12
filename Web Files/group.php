<?php
$ini = parse_ini_file('config.ini');
$link = mysqli_connect($ini['db_host'],$ini['db_user'],$ini['db_password']);
$database = mysqli_select_db($link,$ini['db_name']);

$user = $_GET['username'];
$tables = $ini['mybb_usertable'];

$sql = "SELECT * FROM ". $tables ." WHERE username = '". mysqli_real_escape_string($link,$user) ."'" ;
$result = $link->query($sql);

if ($result->num_rows > 0){
	while($row = $result->fetch_assoc())
	{
		$groups = $row['usergroup'] . $row['additionalgroups'];
		echo $groups;
	}
} 
else
{
	echo "nou"; // User doesn't exist
}


//-----------------------------------------------------
// Coded by /id/Thaisen! Free loader source
// https://github.com/ThaisenPM/Cheat-Loader-CSGO-2.0
// Note to the person using this, removing this
// text is in violation of the license you agreed
// to by downloading. Only you can see this so what
// does it matter anyways.
// Copyright © ThaisenPM 2017
// Licensed under a MIT license
// Read the terms of the license here
// https://github.com/ThaisenPM/Cheat-Loader-CSGO-2.0/blob/master/LICENSE
//-----------------------------------------------------
?>

<head>
<title>Checking groups</title>
</head>
