<?php

// A constant that is only defined here
const _CALLED_FROM_CONTROLLER = true;

require_once 'config.php';

// All queries require a JSON encoded response
require_once "../model/Response.php";
require_once "../model/Request.php";
require_once "Helpers.php";

$path = filter_input(INPUT_SERVER, 'REQUEST_URI');
$method = filter_input(INPUT_SERVER, 'REQUEST_METHOD');
$script = filter_input(INPUT_SERVER, 'SCRIPT_NAME');

// Check for allowed methods
if ($method !== 'GET' && $method !== 'POST' && $method != 'PATCH' 
        && $method != 'DELETE') {
    $response = new Response(
            Response::httpResponseNotAllowed,
            false,
            "Method $method is not allowed.");
    $response->send();
    exit;
}

// Check for valid JSON
if ($method !== 'GET') {
    // Check content type is set to application/json.
    if (filter_input(INPUT_SERVER, 'CONTENT_TYPE') !== 'application/json') {
        $response = new Response(Response::httpResponseBadRequest, false, 
                "Content Type header not set to JSON");
        $response->send();
        exit;
    }

    // Check data is valid JSON.
    $rawPostData = file_get_contents('php://input');
    if (!$jsonData = json_decode($rawPostData)) {
        $response = new Response(Response::httpResponseBadRequest, false, 
                "Request body is not valid JSON");
        $response->send();
        exit;
    }
}

$ds = new Datastore();
        

// check for authorisation
$req = new Request();
if ($req->getAuthenticate() === true) {
    $auth = filter_input(INPUT_SERVER, 'HTTP_AUTHORIZATION');
    $token = ($auth !== null && $auth !== false) ? $auth : 
            filter_input(INPUT_SERVER, 'REDIRECT_HTTP_AUTHORIZATION');

    if (!$token || strlen($token) < 1) {
        $response = new Response(Response::httpResponseUnauthorized, false);
        if ($token === false || $token === null) {
            $response->addMessage("Access token is missing from the header");
        } elseif (strlen($token < 1)) {
            $response->addMessage("Access token cannot be blank");
        }
        $response->send();
        exit;
    }
    
    if($ds->authenticate($token) === false) {
        $response = new Response(Response::httpResponseUnauthorized, false, 
                "Could not authenticate with access token provided");
        $response->send();
        exit;
    }
}


$classname = $req->getClassName();
try {
    require_once "../model/$classname.php";
    $class = new $classname();
    $class->getResult($req->getMethod(), $req->getNode());
} catch (ErrorException $ex) {
    $response = new Response(Response::httpResponseFileNotFound);

    $response->setSuccess(false);
    $response->addMessage("No endpoint found.");
    $response->send();
}

