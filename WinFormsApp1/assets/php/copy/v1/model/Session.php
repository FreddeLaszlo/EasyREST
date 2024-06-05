<?php

/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Scripting/PHPClass.php to edit this template
 */

/**
 * Description of sessions
 *
 * @author fredd
 */
class Session {

    private $_id = null;
    private $_token = null;
    private $_refreshtoken = null;
    private $_sessionid = null;

    function __construct() {
        ini_set("session.use_cookies", 0);
        ini_set("session.use_only_cookies", 0);
        ini_set("session.use_trans_sid", 0);
        ini_set("session.cache_limiter", "");
        $this->_sessionid = filter_input(INPUT_SERVER, 'HTTP_SESSIONID');
    }

    public function CheckSession() {
        if (!$this->_sessionid || strlen($this->_sessionid) < 1) {
            $response = new Response(Response::httpResponseUnauthorized, false);
            if ($this->_sessionid === false || $this->_sessionid === null) { $response->addMessage("Session ID is missing from the header");} 
            elseif (strlen($this->_sessionid) < 1) { $response->addMessage("Session ID cannot be blank"); }
            $response->send();
            exit;
        }

        $auth = filter_input(INPUT_SERVER, 'HTTP_AUTHORIZATION');
        $token = ($auth !== null && $auth !== false) ? $auth : filter_input(INPUT_SERVER, 'REDIRECT_HTTP_AUTHORIZATION');
        if (!$token || strlen($token) < 1) {
            $response = new Response(Response::httpResponseUnauthorized, false);
            if ($token === false || $token === null) { $response->addMessage("Access token is missing from the header"); } 
            elseif (strlen($token) < 1) { $response->addMessage("Access token cannot be blank"); }
            $response->send();
            exit;
        }

        return true;
    }
}
