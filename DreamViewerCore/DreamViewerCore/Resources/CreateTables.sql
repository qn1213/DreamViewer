
CREATE TABLE IF NOT EXISTS "TArtist" (
  "artist" VARCHAR(255) NOT NULL,
  PRIMARY KEY ("artist"));


CREATE TABLE IF NOT EXISTS "TLanguage" (
  "language" VARCHAR(255) NOT NULL DEFAULT 'N/A',
  PRIMARY KEY ("language"))
;


CREATE TABLE IF NOT EXISTS "TType" (
  "type" VARCHAR(255) NOT NULL DEFAULT 'N/A',
  PRIMARY KEY ("type"))
;


CREATE TABLE IF NOT EXISTS "TTitle" (
  "id" VARCHAR(255) NOT NULL,
  "title" VARCHAR(255) NOT NULL,
  "path" VARCHAR(255) NULL,
  "TLanguage_language" VARCHAR(255) NOT NULL,
  "TType_type" VARCHAR(255) NOT NULL,
  PRIMARY KEY ("id"),
  CONSTRAINT "fk_TTitle_TLanguage1"
    FOREIGN KEY ("TLanguage_language")
    REFERENCES "TLanguage" ("language")
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT "fk_TTitle_TType1"
    FOREIGN KEY ("TType_type")
    REFERENCES "TType" ("type")
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;


CREATE TABLE IF NOT EXISTS "Rartist" (
  "TTitle_id" VARCHAR(255) NOT NULL,
  "TArtist_artist" VARCHAR(255) NOT NULL,
  PRIMARY KEY ("TTitle_id", "TArtist_artist"),
  CONSTRAINT "fk_manga_has_artists_artists1"
    FOREIGN KEY ("TArtist_artist")
    REFERENCES "TArtist" ("artist"),
  CONSTRAINT "fk_manga_has_artists_manga1"
    FOREIGN KEY ("TTitle_id")
    REFERENCES "TTitle" ("id"))
;


CREATE TABLE IF NOT EXISTS "TTag" (
  "tag" VARCHAR(255) NOT NULL,
  PRIMARY KEY ("tag"))
;


CREATE TABLE IF NOT EXISTS "Rtag" (
  "TTitle_id" VARCHAR(255) NOT NULL,
  "TTag_tag" VARCHAR(255) NOT NULL,
  PRIMARY KEY ("TTitle_id", "TTag_tag"),
  CONSTRAINT "fk_manga_tags_manga1"
    FOREIGN KEY ("TTitle_id")
    REFERENCES "TTitle" ("id"),
  CONSTRAINT "fk_manga_tags_tags1"
    FOREIGN KEY ("TTag_tag")
    REFERENCES "TTag" ("tag"))
;


CREATE TABLE IF NOT EXISTS "TGroup" (
  "group" VARCHAR(255) NOT NULL,
  PRIMARY KEY ("group"))
;


CREATE TABLE IF NOT EXISTS "TSeries" (
  "series" VARCHAR(255) NOT NULL,
  PRIMARY KEY ("series"))
;


CREATE TABLE IF NOT EXISTS "TCharacter" (
  "character" VARCHAR(255) NOT NULL,
  PRIMARY KEY ("character"))
;


CREATE TABLE IF NOT EXISTS "Rgroup" (
  "TTitle_id" VARCHAR(255) NOT NULL,
  "TGroup_group" VARCHAR(255) NOT NULL,
  PRIMARY KEY ("TTitle_id", "TGroup_group"),
  CONSTRAINT "fk_TGroup_has_TTitle_TGroup1"
    FOREIGN KEY ("TGroup_group")
    REFERENCES "TGroup" ("group")
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT "fk_TGroup_has_TTitle_TTitle1"
    FOREIGN KEY ("TTitle_id")
    REFERENCES "TTitle" ("id")
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;


CREATE TABLE IF NOT EXISTS "Rseries" (
  "TTitle_id" VARCHAR(255) NOT NULL,
  "TSeries_series" VARCHAR(255) NOT NULL,
  PRIMARY KEY ("TTitle_id", "TSeries_series"),
  CONSTRAINT "fk_TTitle_has_TSeries_TTitle1"
    FOREIGN KEY ("TTitle_id")
    REFERENCES "TTitle" ("id")
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT "fk_TTitle_has_TSeries_TSeries1"
    FOREIGN KEY ("TSeries_series")
    REFERENCES "TSeries" ("series")
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;


CREATE TABLE IF NOT EXISTS "Rcharacter" (
  "TTitle_id" VARCHAR(255) NOT NULL,
  "TCharacter_character" VARCHAR(255) NOT NULL,
  PRIMARY KEY ("TTitle_id", "TCharacter_character"),
  CONSTRAINT "fk_TTitle_has_TCharacter_TTitle1"
    FOREIGN KEY ("TTitle_id")
    REFERENCES "TTitle" ("id")
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT "fk_TTitle_has_TCharacter_TCharacter1"
    FOREIGN KEY ("TCharacter_character")
    REFERENCES "TCharacter" ("character")
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;



CREATE TABLE IF NOT EXISTS "TInfo" (
  "name" VARCHAR(255) NOT NULL,
  "value" VARCHAR(255) NOT NULL,
  PRIMARY KEY ("name"))
;
