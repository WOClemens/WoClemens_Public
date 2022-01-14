const app = document.getElementById('root');



const container = document.createElement('div');
container.setAttribute('class', 'container');


app.appendChild(container);

var request = new XMLHttpRequest();
request.open('GET', 'http://localhost:3000/fragen', true);

var request2 = new XMLHttpRequest();
request2.open('GET', 'http://localhost:3000/answer', true);
var data2;

request.onload = function () {
  // Begin accessing JSON data here
  var data = JSON.parse(this.response);
  if (request.status >= 200 && request.status < 400) {
    data.forEach(movie => {

      if(movie.type != "Answer")
      {
        const card = document.createElement('div');
        card.setAttribute('class', 'card');

        const h1 = document.createElement('h2');
        h1.setAttribute('class', 'head')
        h1.textContent = `${movie.type}...${movie.name}` ;

        const h2 = document.createElement('h2')
        h2.textContent = movie.tital

        const p = document.createElement('p');
        movie.description = movie.description.substring(0, 300);
        p.textContent = `${movie.description}...`;

        const answerD = document.createElement('div');
        answerD.setAttribute('class', 'answer');
        answerD.setAttribute('onclick', 'openAnswer()');

        container.appendChild(card);
        card.appendChild(h1);
        card.appendChild(h2);
        card.appendChild(p);
        card.appendChild(answerD);

        data.forEach(answers => {
          if(answers.tital == movie.name && answers.type == "Answer")
          {
            const p2 = document.createElement('p');
            answers.description = answers.description.substring(0, 300);
            p2.textContent = `${answers.name}: ${answers.description}`;
            answerD.appendChild(p2);
          }
        });
      }



    });
  } else {
    const errorMessage = document.createElement('marquee');
    errorMessage.textContent = `Gah, it's not working!`;
    app.appendChild(errorMessage);
  }
}

request.send();

function openAnswer() {
    window.location = "answer.html";
}
