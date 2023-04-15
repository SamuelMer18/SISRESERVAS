// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//preguntas frecuentes
let question = document.querySelectorAll('.question');
let btnDropdown = document.querySelectorAll('.question .more')
let answer = document.querySelectorAll('.answer');
let parrafo = document.querySelectorAll('.answer p');

for (let i = 0; i < btnDropdown.length; i++) {

    let altoParrafo = parrafo[i].clientHeight;
    let switchc = 0;

    btnDropdown[i].addEventListener('click', () => {

        if (switchc == 0) {

            answer[i].style.height = `${altoParrafo}px`;
            question[i].style.marginBottom = '10px';
            btnDropdown[i].innerHTML = '<i>-</i>';
            switchc++;

        }

        else if (switchc == 1) {

            answer[i].style.height = `0`;
            question[i].style.marginBottom = '0';
            btnDropdown[i].innerHTML = '<i>+</i>';
            switchc--;

        }

    })

}