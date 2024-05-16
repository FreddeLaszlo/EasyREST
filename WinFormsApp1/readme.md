# Introduction

A simple project to define RESTful API endpoints via a graphical user interface and, output boilerplate code in a variety of languages. The purpose is to enable quick design and implementation.

There are various other projects that can do the same and this may seem like reinventing the wheel. This is a personal project allowing me to learn a variety of different languages and protocols and, because I wish to implement web APIs in my home automation. The output code is dependant on server capabilities. For example, a NAS could have PHP enabled but a smaller device, such as a Raspberry Pi, may not so could use RUST or Django instead.

# Languages Used

C\# for front end controller and model. C\# webview2, HTML, CSS, SVG and JavaScript for the view. RUST, PHP, C, C++, Django for generated boilerplate code output.

JavaScript used for simple node analysis and GUI layout of endpoint nodes.

Using HTML and SVG allowed the project to use CSS to easily alter the look of the program. SVG was chosen simply because it is a ready-made solution to drawing and enabling clickable elements on screen.

# Must-Haves, Should-Haves and Would-Like-to-Haves

### ‘Must-haves’ are the minimum requirements for the program to be usable.

| Objective                                           | Status                        |
|-----------------------------------------------------|-------------------------------|
| Display endpoint nodes.                             | &#9989;                       |
| Add a path of nodes.                                | &#9989;                       |
| Edit a node for GET/POST/PATCH/DELETE requirements. | &#9989; (may be updated)      |
| Save and load projects.                             | &#9989; (saved as json files) |
| Use CSS for node element design (look and feel).    | &#9989;                       |
| Output boilerplate for PHP                          | In development.               |
| Output boilerplate for RUST                         | In development.               |
| Output boilerplate for Django                       | In development.               |
| Output boilerplate for C                            | In development.               |
| Output boilerplate for C++                          | In development.               |
| Auto generate documentation for the endpoints.      | In development.               |
| Test units                                          | During development.           |

### ‘Should-haves’ are requirements a project should have but are not necessary for a completed project.

| Objective      | Status   |
|----------------|----------|
| Undo and Redo. | &#10060; |
| Insert node    | &#10060; |
| Delete node    | &#10060; |

### ‘Would-like-to-haves’ are additions to the project that are not necessary but would enhance it.

| Objective         | Status   |
|-------------------|----------|
| Database designer | &#10060; |

# Project Model

As projects go, the model is simple. Figure 1 shows the project split into two main areas – C\# and WebView2. C\# Is the interface container and the model. WebView2 is the view.


<figure>
	<img src=media/ad51a8d5c61d0149b836062b448ec5c6.png alt="Figure 1 - Project model.">
	<figcaption>Figure 1 - Project model.</figcaption>
</figure>


# License

In general, any person can download and use this project. Please see the license.txt file that accompanies this project.

# Sample Screens
<figure>
	<img src=media/0ff0cecce8666bd39e5245e51f35722a.png alt="Figure 2 - Add a path from root is simple.">
	<figcaption>Figure 2 - Add a path from root is simple.</figcaption>
</figure>

<figure>
	<img src=media/db29c2093d0ab1e707382b1ef764d586.png alt="Figure 3 - Add a new path that shares another is equally simple.">
	<figcaption>Figure 3 - Add a new path that shares another is equally simple.</figcaption>
</figure>

<figure>
	<img src=media/0540681fb1ead32a3c43de22355714b2.png alt="Figure 4 - A node path that diverges.">
	<figcaption>Figure 4 - A node path that diverges.</figcaption>
</figure>

<figure>
	<img src=media/aa957a7e93178cfaa0f23719db5c075a.png alt="Figure 5 - Each node is clickable, allowing detailed configuration.">
	<img src=media/27e774dff09f801e3532bb51e3b76c35.png alt="Figure 5 - Each node is clickable, allowing detailed configuration.">
	<figcaption>Figure 5 - Each node is clickable, allowing detailed configuration.</figcaption>
</figure>

<figure>
	<img src=media/588e6e8fc6ba7e6eff731cf1130a28ee.png alt="Figure 6 - A complete API can be designed quickly and easily">
	<figcaption>Figure 6 - A complete API can be designed quickly and easily.</figcaption>
</figure>
