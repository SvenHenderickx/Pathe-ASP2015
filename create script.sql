 -- Drop tables
DROP TABLE Film CASCADE CONSTRAINTS;
DROP TABLE Ticket CASCADE CONSTRAINTS;
DROP TABLE Bioscoop CASCADE CONSTRAINTS;
DROP TABLE Zaal CASCADE CONSTRAINTS;
DROP TABLE Stoel CASCADE CONSTRAINTS;
DROP TABLE Regisseur CASCADE CONSTRAINTS;
DROP TABLE Genre CASCADE CONSTRAINTS;
DROP TABLE Film_Genre CASCADE CONSTRAINTS;
DROP TABLE Acteur CASCADE CONSTRAINTS;
DROP TABLE Film_Acteur CASCADE CONSTRAINTs;
DROP TABLE Review CASCADE CONSTRAINTS;
DROP TABLE Account CASCADE CONSTRAINTS;
DROP TABLE Openingstijd CASCADE CONSTRAINTS;
DROP TABLE Voorstelling CASCADE CONSTRAINTS;
DROP TABLE Prijs CASCADE CONSTRAINTS;

-- Aanmaken Table s

CREATE TABLE Regisseur
(
	id number PRIMARY KEY,
	naam varchar2(100) NOT NULL,
	geboorteDatum date,
	woonplaats varchar2(100),
	fotoUrl varchar2(100)
);

CREATE TABLE Film
(
	id number PRIMARY KEY,
	naam varchar2(100) NOT NULL,
	duur number,
	beschrijving varchar2(255) DEFAULT '',
	taalversie varchar2(255),
	releaseDate date,
	regisseur_id number NOT NULL,
	CONSTRAINT Film_fk FOREIGN KEY (regisseur_id) REFERENCES Regisseur(id)
);

CREATE TABLE Bioscoop
(
	id number PRIMARY KEY,
	naam varchar2(100) NOT NULL,
	plaats varchar2(100),
	adres varchar2(100),
	postcode varchar2(100),
	lift number CHECK (lift IN ('0', '1')) /* bool */,
	rolstoelmogelijkheid number CHECK (rolstoelmogelijkheid IN ('0', '1')) /* bool */,
	ringleiding number CHECK (ringleiding IN ('0', '1')) /* bool */,
	geluidssysteem varchar2(100),
	klimaatcontrole number CHECK (klimaatcontrole IN ('0', '1'))  /* bool */
);

CREATE TABLE Openingstijd
(
	id number PRIMARY KEY,
	dag varchar2(50) CHECK(dag IN('maandag', 'dinsdag', 'woensdag', 'donderdag', 'vrijdag', 'zaterdag', 'zondag')),
	openingstijd date,
	sluitingstijd date,
	bioscoop_id number NOT NULL,
	CONSTRAINT Openingstijd_fk FOREIGN KEY (bioscoop_id) REFERENCES Bioscoop(id)
);

CREATE TABLE Zaal
(
	id number PRIMARY KEY,
	nummer number,
	bioscoop_id number NOT NULL,
	CONSTRAINT Zaal_fk FOREIGN KEY (bioscoop_id) REFERENCES Bioscoop(id)
);

CREATE TABLE Stoel
(
	id number PRIMARY KEY,
	type varchar2(100) CHECK (type IN('rolstoel', 'normaal')),
	rij number,
	nummer number,
	xPos number,
	yPos number,
	status varchar2(100) CHECK (status IN ('bezet', 'vrij', 'niet beschikbaar')),
	zaal_id number NOT NULL,
	CONSTRAINT Stoel_fk FOREIGN KEY (zaal_id) REFERENCES Zaal(id)
);

CREATE TABLE Account
(
	id number PRIMARY KEY,
	naam varchar2(100) NOT NULL,
	tussenvoegsel varchar2(100),
	achternaam varchar2(100) NOT NULL,
	geboorteDatum date,
	geslacht varchar2(50) CHECK (geslacht IN ('man', 'vrouw')),
	emailadres varchar2(100) UNIQUE/*moet @ bevatten */,
	wachtwoord varchar2(100),
	nieuwsbrief number CHECK (nieuwsbrief IN ('0', '1')) /*bool*/
);

CREATE TABLE Review
(
	id number PRIMARY KEY,
	tekst varchar2(250),
	aantalSterren number CHECK (aantalSterren IN('0','1', '2', '3', '4', '5'))/* 0-5 */,
	film_id number,
	account_id number,
	CONSTRAINT Review_fk FOREIGN KEY (film_id) REFERENCES Film(id),
	CONSTRAINT Review_fk2 FOREIGN KEY (account_id) REFERENCES Account(id)
);

CREATE TABLE Genre
(
	id number PRIMARY KEY,
	naam varchar2(100)
);

CREATE TABLE Film_Genre
(
	film_id number,
	genre_id number,
	CONSTRAINT film_genre_fk FOREIGN KEY (genre_id) REFERENCES Genre(id),
	CONSTRAINT film_genre_fk2 FOREIGN KEY (film_id) REFERENCES Film(id),
	CONSTRAINT film_genre_pk PRIMARY KEY (genre_id, film_id)
);

CREATE TABLE Acteur
(
	id number PRIMARY KEY,
	naam varchar2(100),
	geboorteDatum date,
	biografie varchar2(250),
	fotoUrl varchar2(100)
);

CREATE TABLE Film_Acteur
(
	acteur_id number,
	film_id number,
	CONSTRAINT film_acteur_fk FOREIGN KEY (acteur_id) REFERENCES Acteur(id),
	CONSTRAINT film_acteur_fk2 FOREIGN KEY (film_id) REFERENCES Film(id),
	CONSTRAINT film_acteur_pk PRIMARY KEY (acteur_id, film_id)
);

CREATE TABLE Voorstelling
(
	id number PRIMARY KEY,
	film_id number,
	zaal_id number,
	tijd date NOT NULL,
	datum date NOT NULL,
	formaat varchar2(100) CHECK(formaat IN ('2D', '3D', 'IMAX')),
	CONSTRAINT voorstelling_fk FOREIGN KEY (film_id) REFERENCES Film(id),
	CONSTRAINT voorstelling_fk2 FOREIGN KEY (zaal_id) REFERENCES Zaal(id)
);


CREATE TABLE Prijs
(
	id number PRIMARY KEY,
	naam varchar2(100),
	prijs number,
	informatie varchar2(250)
);

CREATE TABLE Ticket
(
	id number PRIMARY KEY,
	voorstelling_id number,
	stoel_id number,
	prijs_id number NOT NULL,
	CONSTRAINT ticket_fk FOREIGN KEY (voorstelling_id) REFERENCES Voorstelling(id),
	CONSTRAINT ticket_fk2 FOREIGN KEY (stoel_id) REFERENCES Stoel(id),
	CONSTRAINT ticket_fk3 FOREIGN KEY (prijs_id) REFERENCES Prijs(id)
);

COMMIT;

-- Testdata

-- Regisseur
INSERT INTO Regisseur VALUES('0', 'Sam Mendes', TO_DATE('1965/08/01', 'yyyy/mm/dd'), 'United Kingdom', 'url');
INSERT INTO Regisseur VALUES('1', 'Genndy Tartakovsky', TO_DATE('1970/01/17', 'yyyy/mm/dd'), 'Russia', 'url');
INSERT INTO Regisseur VALUES('2', 'Tim Burton', TO_DATE('1958/08/25', 'yyyy/mm/dd'), 'USA', 'url');

-- Film
INSERT INTO Film VALUES('0', 'Spectre', '151', 'Door een mysterieus bericht wordt Bond geconfronteerd met zijn verleden en moet hij een duistere organisatie ontmaskeren.', 'Engels, Nederlands ondertiteld', TO_DATE('2015/10/29', 'yyyy/mm/dd'), '0');
INSERT INTO Film VALUES('1', 'Hotel Transsylvanie 2', '89', 'Dracula en zijn vrienden zijn weer terug voor een monsterlijk grappig avontuur in Hotel Transsylvanië 2.', 'Nederlands', TO_DATE('2015/10/07', 'yyyy/mm/dd'), '1');
INSERT INTO Film VALUES('2', 'Big Eyes', '106', 'Big Eyes vertelt het waargebeurde verhaal van één van de grootste kunstfraudes van de twintigste eeuw. In de jaren ‘50 en ‘60 krijgt kunstenaar Walter Keane grote bekendheid met zijn raadselachtige schilderijen van kinderen met enorme ogen.', 'Engels, Nederlands ondertiteld', TO_DATE('2015/02/05', 'yyyy/mm/dd'), '2');

-- Acteur
INSERT INTO Acteur VALUES('0', 'Daniel Craig', TO_DATE('1968/03/02', 'yyyy/mm/dd'), 'Daniel Craig is een Britse acteur en is geboren op 2 maart 1968 in Chester. Hij speelt bij het National Youth Theatre in Londen en haalt zijn diploma aan de prestigieuze Guildhall School of Music and Drama.', 'url');
INSERT INTO Acteur VALUES('1', 'Christoph Waltz', TO_DATE('1956/10/04', 'yyyy/mm/dd'), 'Christoph Waltz is een Oostenrijkse acteur, geboren op 4 oktober 1956 in Wenen. Hij speelt in meer dan twintig bioscoopfilms.', 'url');
INSERT INTO Acteur VALUES('2', 'Ben Whishaw', TO_DATE('1980/10/14', 'yyyy/mm/dd'), '', 'url');
INSERT INTO Acteur VALUES('3', 'Charly Luske', TO_DATE('1978/09/19', 'yyyy/mm/dd'), 'Charly Luske is een Nederlandse zanger, acteur en presentator, geboren op 19 september 1978 in Amsterdam. Al op jonge leeftijd neemt hij zanglessen, en in 1994 zingt hij een duet met Gordon.', 'url');
INSERT INTO Acteur VALUES('4', 'Ferry Doedens', TO_DATE('1978/09/19', 'yyyy/mm/dd'), 'Nederlandse acteur', 'url');
INSERT INTO Acteur VALUES('5', 'Amy Adams', TO_DATE('1974/08/20', 'yyyy/mm/dd'), 'Amy Adams is een Amerikaanse actrice, geboren op 20 augustus 1974 in Vicenza, Italië. Ze is bekend van films als Catch Me If You Can, Enchanted en Man of Steel.', 'url');

-- Acteur koppelen aan film
INSERT INTO Film_Acteur VALUES('0', '0');
INSERT INTO Film_Acteur VALUES('1', '0');
INSERT INTO Film_Acteur VALUES('2', '0');
INSERT INTO Film_Acteur VALUES('3', '1');
INSERT INTO Film_Acteur VALUES('4', '1');
INSERT INTO Film_Acteur VALUES('1', '2');
INSERT INTO Film_Acteur VALUES('5', '2');

-- ACCOUNT
INSERT INTO Account VALUES('0', 'Sven', '', 'Henderickx', TO_DATE('1996/09/27', 'yyyy/mm/dd'), 'man', '318943@student.fontys.nl', 'ww1234', '0');
INSERT INTO Account VALUES('1', 'Henk', '', 'Janssen', TO_DATE('1980/02/07', 'yyyy/mm/dd'), 'man', 'henk.janssen@gmail.com', 'henkjanssen1', '1');
INSERT INTO Account VALUES('2', 'Emma', 'de', 'Vries', TO_DATE('1978/05/14', 'yyyy/mm/dd'), 'vrouw', 'e.devries@hotmail.com', 'emmatjuhh8', '0');
INSERT INTO Account VALUES('3', 'Maria', '', 'Netvlies', TO_DATE('1998/12/21', 'yyyy/mm/dd'), 'vrouw', 'maria.netvlies@live.nl', 'mNeTvLiEs', '0');

-- Review
INSERT INTO Review VALUES('0', 'Deze film is zeker de moeite waard om te zien!', '4', '0', '0');
INSERT INTO Review VALUES('1', 'Meer een film voor kinderen, vond er zelf niet zoveel aan.', '2', '1', '2');
INSERT INTO Review VALUES('2', 'Vond het echt een fantastische film. Zeker een aanrader.', '5', '1', '3');
INSERT INTO Review VALUES('3', 'Wauw gewoon een must see.', '5', '0', '3');
INSERT INTO Review VALUES('4', 'Vond de vorige film beter..', '3', '0', '2');
INSERT INTO Review VALUES('5', 'Fantastisch', '5', '2', '1');

-- Bioscoop
INSERT INTO Bioscoop VALUES('0', 'Pathé Amersfoort', 'Amersfoort', 'Eemsplein 2', '3812EA', '1', '1', '0', 'Dolby Digital 7.1', '1');
INSERT INTO Bioscoop VALUES('1', 'Pathé Breda', 'Breda', 'Chasséveld 15', '4811DH', '1', '1', '0', 'Dolby Digital 7.1', '1');

-- Zaal
INSERT INTO Zaal VALUES('0', '1', '0');
INSERT INTO Zaal VALUES('1', '2', '0');
INSERT INTO Zaal VALUES('2', '1', '1');
INSERT INTO Zaal VALUES('3', '2', '1');

-- Stoel
-- Zaal 1 bioscoop amersfoort
INSERT INTO Stoel VALUES('0', 'normaal', '1', '1', '0', '0', 'vrij', '0');
INSERT INTO Stoel VALUES('1', 'rolstoel', '1', '2', '1', '0', 'vrij', '0');
INSERT INTO Stoel VALUES('2', 'normaal', '2', '1', '0', '1', 'bezet', '0');
INSERT INTO Stoel VALUES('3', 'normaal', '2', '2', '1', '1', 'bezet', '0');
INSERT INTO Stoel VALUES('4', 'normaal', '3', '1', '0', '2', 'vrij', '0');
INSERT INTO Stoel VALUES('5', 'normaal', '3', '2', '1', '2', 'vrij', '0');

-- zaal 2 bioscoop amersfoort
INSERT INTO Stoel VALUES('6', 'normaal', '1', '1', '0', '0', 'bezet', '1');
INSERT INTO Stoel VALUES('7', 'rolstoel', '1', '2', '1', '0', 'bezet', '1');
INSERT INTO Stoel VALUES('8', 'normaal', '2', '1', '0', '1', 'bezet', '1');
INSERT INTO Stoel VALUES('9', 'normaal', '2', '2', '1', '1', 'vrij', '1');
INSERT INTO Stoel VALUES('10', 'normaal', '3', '1', '0', '2', 'vrij', '1');
INSERT INTO Stoel VALUES('11', 'normaal', '3', '2', '1', '2', 'vrij', '1');

-- zaal 1 bioscoop breda
INSERT INTO Stoel VALUES('12', 'normaal', '1', '1', '0', '0', 'bezet', '2');
INSERT INTO Stoel VALUES('13', 'normaal', '1', '2', '1', '0', 'vrij', '2');
INSERT INTO Stoel VALUES('14', 'normaal', '2', '1', '0', '1', 'vrij', '2');
INSERT INTO Stoel VALUES('15', 'normaal', '2', '2', '1', '1', 'vrij', '2');
INSERT INTO Stoel VALUES('16', 'normaal', '3', '1', '0', '2', 'vrij', '2');
INSERT INTO Stoel VALUES('17', 'normaal', '3', '2', '1', '2', 'vrij', '2');

-- zaal 2 bioscoop breda
INSERT INTO Stoel VALUES('18', 'rolstoel', '1', '1', '0', '0', 'vrij', '3');
INSERT INTO Stoel VALUES('19', 'rolstoel', '1', '2', '1', '0', 'vrij', '3');
INSERT INTO Stoel VALUES('20', 'normaal', '2', '1', '0', '1', 'bezet', '3');
INSERT INTO Stoel VALUES('21', 'normaal', '2', '2', '1', '1', 'vrij', '3');
INSERT INTO Stoel VALUES('22', 'normaal', '3', '1', '0', '2', 'vrij', '3');
INSERT INTO Stoel VALUES('23', 'normaal', '3', '2', '1', '2', 'vrij', '3');
INSERT INTO Stoel VALUES('24', 'rolstoel', '3', '2', '2', '2', 'vrij', '3');

-- openingstijd
INSERT INTO Openingstijd VALUES('0', 'maandag', TO_DATE('10/45', 'HH24/MI'), TO_DATE('22/30', 'HH24/MI'), '0');
INSERT INTO Openingstijd VALUES('1', 'dinsdag', TO_DATE('10/45', 'HH24/MI'), TO_DATE('22/30', 'HH24/MI'), '0');
INSERT INTO Openingstijd VALUES('2', 'woensdag', TO_DATE('10/45', 'HH24/MI'), TO_DATE('22/30', 'HH24/MI'), '0');
INSERT INTO Openingstijd VALUES('3', 'donderdag', TO_DATE('10/45', 'HH24/MI'), TO_DATE('22/30', 'HH24/MI'), '0');
INSERT INTO Openingstijd VALUES('4', 'vrijdag', TO_DATE('10/30', 'HH24/MI'), TO_DATE('23/00', 'HH24/MI'), '0');
INSERT INTO Openingstijd VALUES('5', 'zaterdag', TO_DATE('10/30', 'HH24/MI'), TO_DATE('23/00', 'HH24/MI'), '0');
INSERT INTO Openingstijd VALUES('6', 'zondag', TO_DATE('10/30', 'HH24/MI'), TO_DATE('23/00', 'HH24/MI'), '0');

INSERT INTO Openingstijd VALUES('7', 'maandag', TO_DATE('11/00', 'HH24/MI'), TO_DATE('23/00', 'HH24/MI'), '0');
INSERT INTO Openingstijd VALUES('8', 'dinsdag', TO_DATE('11/00', 'HH24/MI'), TO_DATE('23/00', 'HH24/MI'), '0');
INSERT INTO Openingstijd VALUES('9', 'woensdag', TO_DATE('11/00', 'HH24/MI'), TO_DATE('23/00', 'HH24/MI'), '0');
INSERT INTO Openingstijd VALUES('10', 'donderdag', TO_DATE('11/00', 'HH24/MI'), TO_DATE('23/00', 'HH24/MI'), '0');
INSERT INTO Openingstijd VALUES('11', 'vrijdag', TO_DATE('11/00', 'HH24/MI'), TO_DATE('23/00', 'HH24/MI'), '0');
INSERT INTO Openingstijd VALUES('12', 'zaterdag', TO_DATE('10/00', 'HH24/MI'), TO_DATE('23/00', 'HH24/MI'), '0');
INSERT INTO Openingstijd VALUES('13', 'zondag', TO_DATE('10/00', 'HH24/MI'), TO_DATE('23/00', 'HH24/MI'), '0');

-- genre
INSERT INTO Genre VALUES('0', 'Actie');
INSERT INTO Genre VALUES('1', 'Avontuur');
INSERT INTO Genre VALUES('2', 'Thriller');
INSERT INTO Genre VALUES('3', 'Animatie');
INSERT INTO Genre VALUES('4', 'Kinderfilm');
INSERT INTO Genre VALUES('5', 'Biografie');
INSERT INTO Genre VALUES('6', 'Drama');
INSERT INTO Genre VALUES('7', 'PAC');
INSERT INTO Genre VALUES('8', 'Comedy');

-- Genre's toevoegen aan film
INSERT INTO Film_Genre VALUES('0', '0');
INSERT INTO Film_Genre VALUES('0', '1');
INSERT INTO Film_Genre VALUES('0', '2');
INSERT INTO Film_Genre VALUES('0', '6');

INSERT INTO Film_Genre VALUES('1', '3');
INSERT INTO Film_Genre VALUES('1', '4');

INSERT INTO Film_Genre VALUES('2', '5');
INSERT INTO Film_Genre VALUES('2', '6');
INSERT INTO Film_Genre VALUES('2', '7');

-- Prijs
INSERT INTO Prijs VALUES('0', 'Normaal', '10,50', '');
INSERT INTO Prijs VALUES('1', 'Kinderticket', '5,00', 'Ticket voor kinderen t/m 11 jaar.');
INSERT INTO Prijs VALUES('2', 'Tienerticket', '7,50', 'Ticket voor tieners van 12 t/m 17 jaar.');
INSERT INTO Prijs VALUES('3', 'CJP-korting', '8,50', 'Alle dagen geldig, behalve Vr-Za na 18.00 uur, op feestdagen en in kerstvakantie.');
INSERT INTO Prijs VALUES('4', 'Studentenkorting', '8,50', 'Alle dagen geldig, behalve Vr-Za na 18.00 uur, op feestdagen en in kerstvakantie.');
INSERT INTO Prijs VALUES('5', 'Seniorticket', '8,50', 'Alle dagen geldig, behalve Vr-Za na 18.00 uur, op feestdagen en in kerstvakantie.');
INSERT INTO Prijs VALUES('6', 'Familieticket', '28,00', 'Geldig voor 4 personen "("twee t/m 11 jaar en twee vanaf 18 jaar")".');
INSERT INTO Prijs VALUES('7', 'Ochtendvoordeel', '8,00', 'Geldig bij voorstelling die starten voor 12.00 uur.');

-- Voorstelling
INSERT INTO Voorstelling VALUES('0', '0', '0', TO_DATE('20/00', 'HH24/MI'), TO_DATE('2015/10/27', 'yyyy/mm/dd'), 'IMAX');
INSERT INTO Voorstelling VALUES('1', '1', '2', TO_DATE('18/00', 'HH24/MI'), TO_DATE('2015/10/25', 'yyyy/mm/dd'), '3D');
INSERT INTO Voorstelling VALUES('2', '2', '3', TO_DATE('21/00', 'HH24/MI'), TO_DATE('2015/10/25', 'yyyy/mm/dd'), '2D');

-- Ticket
INSERT INTO Ticket VALUES('0', '0', '0', '0');
INSERT INTO Ticket VALUES('1', '0', '1', '2');
INSERT INTO Ticket VALUES('2', '0', '5', '0');

INSERT INTO Ticket VALUES('3', '1', '7', '1');
INSERT INTO Ticket VALUES('4', '1', '9', '4');
INSERT INTO Ticket VALUES('5', '2', '15', '0');

COMMIT;