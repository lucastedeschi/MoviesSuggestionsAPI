<h1>MovieSuggestions</h1>
<p>

<ul>
  <li>Amazon Elastic Beanstalk Application: <a>http://gimmesuggestions.us-west-2.elasticbeanstalk.com/<a/></li>
</ul>
</p>

<br />
<h2>Exemplos de requests</h2>
<h3>Sugestões</h3>

<ul>
<li>Solicitar sugestões:</li>
<p>
GET: api/suggestions/?userEmail=01@user.com&audio=Quero assistir comedia e ação</br>
RESPONSE: </br>
<code>
[
    {
        "vote_count": 4331,
        "id": 166426,
        "trailer": null,
        "vote_average": 6.5,
        "title": "Piratas do Caribe: A Vingança de Salazar",
        "popularity": 59.487851,
        "poster_path": "/q8zYVAvuPI3ImOkC1EsjDs7ilTk.jpg",
        "original_title": "Pirates of the Caribbean: Dead Men Tell No Tales",
        "genre_ids": [
            12,
            28,
            14,
            35
        ],
        "backdrop_path": "/7C921eWK06n12c1miRXnYoEu5Yv.jpg",
        "adult": false,
        "overview": "O Capitão Jack, que anda passando por uma onda de azar, sente os ventos da má sorte soprando com muita força quando os marinheiros fantasmas assassinos, liderados pelo aterrorizante Capitão Salazar (Javier Bardem), escapam do triângulo do diabo decididos a matar todos os piratas em seu caminho, especialmente Jack.",
        "release_date": "2017-05-23T00:00:00"
    },
    {
        "vote_count": 6579,
        "id": 315635,
        "trailer": null,
        "vote_average": 7.3,
        "title": "Homem-Aranha: De Volta ao Lar",
        "popularity": 59.234227,
        "poster_path": "/iLhIoKnj7G9I5NyknnS2YAxMizS.jpg",
        "original_title": "Spider-Man: Homecoming",
        "genre_ids": [
            28,
            12,
            35,
            878
        ],
        "backdrop_path": "/vc8bCGjdVp0UbMNLzHnHSLRbBWQ.jpg",
        "adult": false,
        "overview": "Depois de atuar ao lado dos Vingadores, chegou a hora do pequeno Peter Parker (Tom Holland) voltar para casa e para a sua vida, já não mais tão normal. Lutando diariamente contra pequenos crimes nas redondezas, ele pensa ter encontrado a missão de sua vida quando o terrível vilão Abutre (Michael Keaton) surge amedrontando a cidade. O problema é que a tarefa não será tão fácil como ele imaginava.",
        "release_date": "2017-07-05T00:00:00"
    }
]
</code>
</p>
  
<li>Solicitar sugestões "Estou com sorte":</li>
<p>
GET: api/lucky/?userEmail=01@user.com&time=02:10</br>
RESPONSE: </br>
<code>
  {
    "vote_count": 1989,
    "id": 396422,
    "trailer": null,
    "vote_average": 6.4,
    "title": "Annabelle 2: A Criação do Mal",
    "popularity": 105.500198,
    "poster_path": "/AtlcH0f8mEQGKVIBSgV4GgkTZMr.jpg",
    "original_title": "Annabelle: Creation",
    "genre_ids": [
        27,
        9648,
        53
    ],
    "backdrop_path": "/3L5gfIKt2RK9vnCiLgWTAzkhQWC.jpg",
    "adult": false,
    "overview": "Anos após a trágica morte de sua filha, um habilidoso artesão de bonecas e sua esposa decidem, por caridade, acolher em sua casa uma freira e dezenas de meninas desalojadas de um orfanato. Atormentado pelas lembranças traumáticas, o casal ainda precisa lidar com um amedrontador demônio do passado: Annabelle, criação do artesão.",
    "release_date": "2017-08-03T00:00:00"
}
  </code>
</p>

<li>Solicitar trailer:</li>
<p>
GET: api/lucky/?movieName=batman</br>
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
