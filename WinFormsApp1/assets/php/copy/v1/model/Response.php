<?php

/**
 * File:            Response.php
 * Author:          Fred de Laszlo
 * Adapted from:    Spinks, M. (2020) - see readme.txt
 * Version:         1.0.0
 * Date:            17-05-2023
 * Copyright:       (C) Fred de Laszlo, 2024
 * 
 * Class to handle a single Response
 */
// Check this script is being called from controller
if (!defined('_CALLED_FROM_CONTROLLER')) {
    exit();
}

class Response {

    private $_success;
    private $_httpStatusCode;
    private $_messages = array();
    private $_data;
    private $_toCache = false;
    private $_responseData = array();

    const httpResponseOK = 200;
    const httpResponseBadRequest = 400;
    const httpResponseUnauthorized = 401;
    const httpResponseForbidden = 403;
    const httpResponseFileNotFound = 404;
    const httpResponseNotAllowed = 405;
    const httpResponseInternalServerError = 500;
    const httpResponseNotImplemented = 501;

    /**
     * 
     * @param int $httpStatusCode - optional status code
     * @param bool $success - optional true or false
     * @param string $message - optional message
     * @param object $data - optional JSON formatted data
     */
    public function __construct($httpStatusCode = null, $success = null, $message = null, $data = null) {
        if ($httpStatusCode !== null) {
            $this->setHttpStatusCode($httpStatusCode);
        }
        if ($success !== null) {
            $this->setSuccess($success);
        }
        if ($message !== null) {
            $this->addMessage($message);
        }
        if ($data !== null) {
            $this->setData($data);
        }
    }

    public function setSuccess($success) {
        $this->_success = $success;
    }

    public function setHttpStatusCode($httpStatusCode) {
        $this->_httpStatusCode = $httpStatusCode;
    }

    public function addMessage($message) {
        $this->_messages[] = $message;
    }

    public function setData($data) {
        $this->_data = $data;
    }

    public function toCache($toCache) {
        $this->_toCache = $toCache;
    }

    public function send() {
        header('Content-type: application/json;charset=utf-8');
        if ($this->_toCache == true) {
            header('Cache-control: max-age=60');
        } else {
            header('Cache-control: no-cache, no-store');
        }

        if (($this->_success !== false && $this->_success !== true) || !is_numeric($this->_httpStatusCode)) {
            http_response_code(self::httpResponseInternalServerError);
            $this->_responseData['statusCode'] = 500;
            $this->_responseData['success'] = false;
            $this->addMessage("Response creation error");
            $this->_responseData['messages'] = $this->_messages;
        } else {
            http_response_code($this->_httpStatusCode);
            $this->_responseData['statusCode'] = $this->_httpStatusCode;
            $this->_responseData['success'] = $this->_success;
            $this->_responseData['messages'] = $this->_messages;
            $this->_responseData['data'] = $this->_data;
        }
        echo json_encode($this->_responseData);
    }
}
