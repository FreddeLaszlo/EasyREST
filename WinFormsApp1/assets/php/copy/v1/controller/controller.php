<?php

// A constant that is only defined here
const _CALLED_FROM_CONTROLLER = true;

require_once 'config.php';

// All queries require a JSON encoded response
require_once "../model/Response.php";
require_once "../model/Request.php";
require_once "Helpers.php";

// Get request details
try {
    $req = new Request();
} catch (ErrorException $ex) {
    $response = new Response(Response::httpResponseBadRequest, false);
    $response->addMessage($ex->getMessage());
    $response->send();
    exit;
}

// check authorisation
if ($req->getAuthenticate() === true) {
    require_once '../model/Session.php';
    $session = new Session();
    $session->CheckSession();
}

// Check for vaild JSON body
$jsonData = [];
if (filter_input(INPUT_SERVER, 'CONTENT_TYPE') !== 'application/json') {
    $response = new Response(Response::httpResponseBadRequest, false,
            "Content Type header not set to JSON");
    $response->send();
    exit;
}
$rawData = file_get_contents('php://input');
if (strlen($rawData) > 0 and !$jsonData = json_decode($rawData, true)) {
    $response = new Response(Response::httpResponseBadRequest, false,
            "Request body is not valid JSON");
    $response->send();
    exit;
}



// All checks for authorisation and valid JSON have passed.
// Collect expected and any optional parameters
$params = $req->getNode()["Expects{$req->getMethod()}"];
foreach ($_GET as $key => $value) {
    $jsonData[$key] = $value;
}
foreach ($req->getNode()['parameters'] as $key => $value) {
    $jsonData[$key] = $value;
}

$messages = [];
foreach ($params as $key => $value) {
    if (!array_key_exists($key, $jsonData)) {
        $messages[] = "'$key' is missing.";
    } elseif ($params[$key] === 'string' && strlen($jsonData[$key]) < 1) {
        $messages[] = "'$key' cannot be blank.";
    } elseif ($params[$key] === 'int' && !is_int($jsonData[$key])) {
        $messages[] = "'$key' is expecting an integer.";
    }
}
if (count($messages) > 0) {
    $response = new Response(Response::httpResponseBadRequest, false);
    $response->addMessage($messages);
    $response->send();
    exit;
}

//Try to call the path node class for processing
$classname = $req->getClassName();
try {
    require_once "../model/paths/$classname.php";
    $class = new $classname();
    $class->getResult($req->getMethod(), $req->getNode(), $jsonData);
} catch (ErrorException $ex) {
    $response = new Response(Response::httpResponseFileNotFound);
    $response->setSuccess(false);
    $response->addMessage("No endpoint found.");
    $response->send();
}

