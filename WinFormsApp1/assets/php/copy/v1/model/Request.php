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

/**
 * Description of Request
 *
 * @author fredd
 */
class Request {

    private $_nodes = null;
    private $_method = '';
    private $_node = null;
    private $_authenticate = false;

    public function getClassName() {
        $return = '';
        if ($this->_node) {
            if ($this->_node['Noun'] === '/') {
                $return = "root_{$this->_node['ID']}";
            } else {
                $return = "{$this->_node['Noun']}_{$this->_node['ID']}";
            }
        }
        return $return;
    }

    public function getMethod() {
        return $this->_method;
    }

    public function getNode() {
        return $this->_node;
    }
    
    public function getAuthenticate() {
        return $this->_authenticate;
    }

    function __construct() {

        $this->_method = filter_input(INPUT_SERVER, "REQUEST_METHOD");
        $uri = filter_input(INPUT_SERVER, 'REQUEST_URI');
        $path = trim(explode('?', $uri)[0], '/');
        $script = trim(filter_input(INPUT_SERVER, 'SCRIPT_NAME'), '/');
        $nodepath = trim(str_replace('controller/controller.php', '', $script), '/');

        $get_rid_of = explode('/', $nodepath);
        foreach ($get_rid_of as $rid) {
            $path = trim(preg_replace("/^$rid/m", '', $path), '/');
        }

        try {
            require_once '../model/nodes.php';
        } catch (ErrorException) {
            $this->nodes = array();
        }
        $this->_node = $this->getNodeFromPath($path);
        $this->authenticate();
    }
    
    private function authenticate() {
        if($this->_node !== null) {
            switch ($this->_method) {
                case 'GET':
                    $this->_authenticate = $this->_node['UseGETAuthorisation'] && $this->_node['UseGET'];
                    break;
                case 'POST':
                    $this->_authenticate = $this->_node['UsePOSTAuthorisation'] && $this->_node['UsePOST'];
                    break;
                case 'PATCH':
                    $this->_authenticate = $this->_node['UsePATCHAuthorisation'] && $this->_node['UsePATCH'];
                    break;
                case 'DELETE':
                    $this->_authenticate = $this->_node['UseDELETEAuthorisation'] && $this->_node['UseDELETE'];
                    break;
            }
        }
    }

    private function getNodeFromPath($path) {
        $parameters = [];

        $previousNoun = '';
        try {
            $currentnode = $this->findNodes()[0];  // Only one root should be returned
        } catch (ErrorException) {
            $currentnode = array();
        }
        if ($path === '') {
            return $currentnode;
        }
        $explodedPath = explode('/', $path);

        for ($i = 0; $i < count($explodedPath); $i++) {
            $foundnodes = $this->findNodes($currentnode['ID']);
            [$id, $noun] = $this->matchNode($foundnodes, $explodedPath[$i]);
            //Helpers::debug_to_console($id);
            if (!$id && !$noun) {
                return [];
            }
            if ($id && $i < count($explodedPath) - 1) {
                $parameters["$previousNoun}_id"] = $explodedPath[$i];
            } elseif ($id) {
                $parameters['id'] = $explodedPath[$i];
            } elseif ($noun) {
                $previousNoun = $noun['Noun'] === '/' ? 'root' : $noun['Noun'];
            }
            $currentnode = $noun ? $noun : $id;
        }

        $currentnode["parameters"] = $parameters;
        return $currentnode;
    }

    private function matchNode($nodes, $pathItem) {
        $id = null;
        foreach ($nodes as $node) {
            if ($node['Noun'] === 'id') {
                $id = $node;
            } elseif ($node['Noun'] === $pathItem) {
                return [null, $node];
            }
        }
        return [$id, null];
    }

    private function findNodes($prevNode = -1) {
        if ($this->_nodes !== null) {
            return array_filter($this->_nodes['Items'], fn($v) => $v['PreviousNodeID'] == $prevNode);
        }
        return array();
    }
}
