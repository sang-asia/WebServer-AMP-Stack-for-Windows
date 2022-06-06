# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.1.7.3] - 2021-07-12
### Fixed
- Fix CDN link of softwares.

## [0.1.7.2] - 2021-06-21
### Fixed
- Load PHP extension "opcache" use "zend_extension" directive.

## [0.1.7.1] - 2021-02-07
### Fixed
- Change MySQL download link.

## [0.1.7] - 2021-01-28
### Added
- Auto-remove PHP Wrapper from Apache config if wrapper does not exist anymore.

### Fixed
- Spacing characters in path used in FcgidWrapper must be escaped with backslash.

## [0.1.6] - 2021-01-28
### Added
- Add icons.
- Add log watcher.
- Add log function for Apache.
- Add log function for MySQL.
- Add log function for MariaDB.
- Add log function for MongoDB.
- Check website existence while opening website config.
- Change Server's name after installed.
- Check SSL cert & key file path.
- Support website outside "Websites" folder.
- Write log to file.

### Changed
- Validate Apache SSL config directive-by-derective, only change config file if needed.

### Fixed
- Can not add SSL support when create.
- Apache auto-fix path error.
- Apache can not install if websites have missing PHP versions.
- Show port can not be zero message if Apache not installed.
- Wrong URL when launch website.
- Can not install PHP versions.
- Infinitive loop when replace config.

## [0.1.5] - 2021-01-24
### Added
- Add HTTPS support for Apache.
- Add Edit action in website Actions menu.

### Changed
- Click website name will open website in default Browser.

## [0.1.4] - 2021-01-23
### Added
- Add PHP config.

## [0.1.3] - 2021-01-23
### Added
- Add MongoDB.

### Changed
- Reduce text "x64 Only" to "x64".

## [0.1.2] - 2021-01-22
### Added
- Auto create missing service while starting server.
- Add change port function for Apache, MySQL, MariaDB.
- Set default password for MySQL while installing.
- Map public folder inside, instead of root public_html folder.
- Auto create hosts file entry for website.

## [0.1.1] - 2021-01-14
### Changed
- Optimize AppBase functionality code.
- Optimize UI when deleting a website.

## [0.1.0] - 2021-01-12
### Added
- Install/Uninstall Apache, MySQL, MariaDB.
- Install/Uninstall PHP Versions.
- Install/Uninstall servers as a Windows service.
- Manage websites with different PHP versions.

### Changed
- Reformat websites list.
- Download archives from SourceForge.
