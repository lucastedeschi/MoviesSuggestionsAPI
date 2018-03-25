<h1>MovieSuggestions</h1>
<p>

<ul>
  <li>Azure: <a>http://gimmesuggestions.us-west-2.elasticbeanstalk.com/<a/></li>
</ul>
</p>

<br />
<h2>Exemplos de requests</h2>
<h3>Sugestões</h3>

<ul>
<li>Solicitar sugestões:</li>
<p>
GET: /suggestions/?userEmail=[example]&audio=[audio]
</p>
  
<li>Solicitar sugestões "Estou com sorte":</li>
<p>
GET: /suggestions/lucky/?userEmail=[example]&time=[horario(Formato: HH:mm)]
</p>

<li>Solicitar trailer:</li>
<p>
GET: api/lucky/?movieName=batman
</p>
<p>
RESPONSE:</br>
  <code>
    {
    "id": "EXeTwQWrcwY",
    "url": "https://www.youtube.com/watch?v=EXeTwQWrcwY",
    "title": "The Dark Knight (2008) Official Trailer #1 - Christopher Nolan Movie HD",
    "thumbnail": "https://i.ytimg.com/vi/EXeTwQWrcwY/hqdefault.jpg"
    }</code>
</p>

 </ul>
