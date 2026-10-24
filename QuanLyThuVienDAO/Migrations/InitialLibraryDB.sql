CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;
CREATE TABLE `Categories` (
    `CategoryId` int NOT NULL AUTO_INCREMENT,
    `Description` varchar(255) NOT NULL,
    PRIMARY KEY (`CategoryId`)
);

CREATE TABLE `Books` (
    `BookId` int NOT NULL AUTO_INCREMENT,
    `Title` varchar(255) NOT NULL,
    `Publisher` varchar(150) NOT NULL,
    `Price` decimal(18,2) NOT NULL,
    `CopyCount` int NOT NULL,
    `Author` varchar(150) NOT NULL,
    `PublishYear` int NOT NULL,
    `CategoryId` int NOT NULL,
    PRIMARY KEY (`BookId`),
    CONSTRAINT `FK_Books_Categories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `Categories` (`CategoryId`) ON DELETE RESTRICT
);

CREATE TABLE `BookCopies` (
    `BookCopyId` int NOT NULL AUTO_INCREMENT,
    `Status` varchar(50) NOT NULL,
    `Condition` varchar(100) NOT NULL,
    `BookId` int NOT NULL,
    PRIMARY KEY (`BookCopyId`),
    CONSTRAINT `FK_BookCopies_Books_BookId` FOREIGN KEY (`BookId`) REFERENCES `Books` (`BookId`) ON DELETE CASCADE
);

CREATE INDEX `IX_BookCopies_BookId` ON `BookCopies` (`BookId`);

CREATE INDEX `IX_Books_CategoryId` ON `Books` (`CategoryId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20261024040314_InitialLibraryDB', '10.0.13');

COMMIT;

