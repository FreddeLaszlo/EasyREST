<?php

/*
 * Author:  Fred de Laszlo
 * Date:    17-05-2024
 * Version: 1.0.0
 * 
 * Copyright (c) Fred de Laszlo, 2024
 * 
 * Setup file
 */

//Setup error reporting level
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL); 


// Deal with CORS requests
// Allow from any origin
if (filter_input(INPUT_SERVER, "REQUEST_METHOD") == 'OPTIONS') {
    header('Access-Control-Allow-Methods: GET, POST, PATCH, DELETE');
    header('Access-Control-Allow-Headers: Content-Type, Authorization');
    header('Access-Control-Max-Age: 86400');
    exit();
}




/*
//Define host address
const __HOST = "http://192.168.0.40/audiogram/";

// Find the document root to load included or required files
define('_BASE_DIR', $_SERVER['DOCUMENT_ROOT'] . '/audiogram/');

// Make sure this file is called from an allowed script.
if(!defined('_PERMISSABLE')) {
   require_once _BASE_DIR . 'not_authorised.php';
}

// Database connections
// If the read and write databases are the same,
// enter the same deatils for both
const _DB_WRITE_HOST = 'localhost';
const _DB_WRITE_NAME = 'audiogramsdb';
const _DB_WRITE_USERNAME = 'root';
const _DB_WRITE_PASSWORD = 'awtiaveu1';

const _DB_READ_HOST = 'localhost';
const _DB_READ_NAME = 'audiogramsdb';
const _DB_READ_USERNAME = 'root';
const _DB_READ_PASSWORD = 'awtiaveu1';
//Token authorisation keys expiry in seconds
const _TOKEN_ACCESS_EXPIRY = 1200;
const _TOKEN_REFRESH_EXPIRY = 1209600;
//Cache time out in seconds
const _CACHE_TIMOUT = 10;
// The error log
const _LOG_FILE = '../err.log';
// Number of allowed login attempts
const _LOGIN_ATTEMPTS = 3;
// Login delay in seconds (prevent brute attacks)
const _LOGIN_DELAY = 1;
// Number of items to display per page
const _DISPLAY_NUM_PER_PAGE = 10;
// Verify token expiry in seconds
const _VERIFY_TOKEN_EXPIRY = 86400;         // 24 hours
// Password reset token expiry
const _PASSWORD_RESET_TOKEN_EXPIRY = 600;   // 10 minutes

// Contact
const __CONTACT_EMAIL = 'query@audiogramfilter.net';
const __NOREPLY_EMAIL = 'noreply@audiogramfilter.net';

*/