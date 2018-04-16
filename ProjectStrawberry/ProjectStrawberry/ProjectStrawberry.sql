CREATE DATABASE ProjectStrawberry;

USE ProjectStrawberry;

CREATE TABLE Gender
(
	Id int primary key identity(1,1),
	Name nvarchar(MAX) not null,
);

INSERT INTO Gender VALUES
('Female'),
('Male')

CREATE TABLE Class
(
	Id int primary key identity(1,1),
	ClassName nvarchar(MAX) not null
)

INSERT INTO Class VALUES
('Assassin'),
('Bounty Hunter'),
('Crusader'),
('Duelist'),
('Fighter'),
('Guard'),
('Thief')

CREATE TABLE "Character"
(
	Id int primary key identity(1,1),
	AccountId nvarchar(128) not null references AspNetUsers(Id),
	-- GENERAL
	Name nvarchar(14) not null,
	GenderId int not null references Gender(Id),
	ClassId int not null references Class(Id),
	Avatar nvarchar(MAX) not null,
	Gold int not null,
	"Level" int not null,
	"Experience" int not null,
	-- OFFENSIVE
	Stamina int not null,
	Strength int not null,
	Quickness int not null, 
	-- DEFENSIVE
	Block int not null,
	Evasion int not null,
	Parry int not null,
	Vitality int not null,
	Health int not null,
	-- WEAPON PROFECIENCY
	Sword int not null,
	Mace int not null,
	Dagger int not null,
	Spear int not null,
	Axe int not null,
	Polearm int not null,

	Alive bit not null,
);

CREATE TABLE WeaponType
(
	Id int not null primary key identity(1,1),
	Name nvarchar(MAX) not null,
);

INSERT INTO WeaponType VALUES
('Axe'),
('Dagger'),
('Mace'),
('Polearm'),
('Spear'),
('Sword')

CREATE TABLE Weapon
(
	Id int not null primary key identity(1,1),
	Name nvarchar(MAX) not null,
	WeaponTypeId int not null REFERENCES WeaponType(Id),
	MinimumDamage int not null,
	MaximumDamage int not null,
	ReqWeaponMastery int not null,
	"Weight" int not null,
	TournamentReward bit not null,
	TwoHanded bit not null,
	Price int not null,
);

CREATE TABLE Shield
(
	Id int not null primary key identity(1,1),
	Name nvarchar(MAX) not null,
	BlockValue int not null,
	ReqShieldMastery int not null,
	"Weight" int not null,
	TourmanemntReward bit not null,
	Price int not null,
);

INSERT INTO Shield VALUES
('Small Shield', '4', '12', '1', '0', '20')

CREATE TABLE ArmorType
(
	Id int not null primary key identity(1,1),
	Name nvarchar(MAX) not null,
);

INSERT INTO ArmorType VALUES
('Head'),
('Shoulder'),
('Torso'),
('Gloves'),
('Legs'),
('Feet')

CREATE TABLE Armor
(
	Id int not null primary key identity(1,1),
	Name nvarchar(MAX) not null,
	ArmorTypeId int not null REFERENCES ArmorType(Id),
	ArmorValue int not null,
	"Weight" int not null,
	TourmanemntReward bit not null,
	Price int not null,
);

INSERT INTO Armor VALUES
('Cloth', '1', '1', '1', '0', '15' ),
('Cloth', '2', '1', '1', '0', '15' ),
('Cloth', '3', '1', '1', '0', '15' ),
('Cloth', '4', '1', '1', '0', '10' ),
('Cloth', '5', '1', '1', '0', '15' ),
('Cloth', '6', '1', '1', '0', '10' )

CREATE TABLE EquipmentCharacter
(
	Id	int not null primary key identity(1,1),
	CharacterId int not null references "Character"(Id),
	WeaponId int references Weapon(Id),
	ShieldId int references Shield(Id),
	ArmorId int references Armor(Id),
	MainHandEquipped bit not null,
	OffHandEquipped bit not null,
	Equipped bit not null,
)

CREATE TABLE Arena
(
	Id int not null primary key identity(1,1),
	CharacterId int not null REFERENCES "Character"(Id),
	Forfeit int not null,
	Queued datetime not null,
);

CREATE TABLE CombatLog
(
	Id int not null primary key identity(1,1),
	Player int references "Character"(Id),
	Spoiler nvarchar(max),
	GameDate datetime not null,
	"Log" varbinary(max) not null,
);