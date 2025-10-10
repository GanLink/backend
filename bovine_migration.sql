CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;
CREATE TABLE `bovinue_cattle_health_records` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `activity_name` varchar(200) NOT NULL,
    `frecuency` int NOT NULL,
    `description` varchar(500) NOT NULL,
    `deleted` tinyint(1) NOT NULL,
    `created_at` datetime NULL,
    `updated_at` datetime NULL,
    PRIMARY KEY (`id`)
);

CREATE TABLE `bovinue_metric_categories` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `category` varchar(100) NOT NULL,
    `deleted` tinyint(1) NOT NULL,
    `created_at` datetime NULL,
    `updated_at` datetime NULL,
    PRIMARY KEY (`id`)
);

CREATE TABLE `users` (
    `id` int NOT NULL AUTO_INCREMENT,
    `username` varchar(10) NOT NULL,
    `firstname` varchar(10) NOT NULL,
    `lastname` varchar(20) NOT NULL,
    `email` varchar(20) NOT NULL,
    `password` longtext NOT NULL,
    `created_at` datetime NULL,
    `updated_at` datetime NULL,
    PRIMARY KEY (`id`)
);

CREATE TABLE `bovinue_metric_parameters` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `category_id` bigint NOT NULL,
    `parameter` varchar(100) NOT NULL,
    `description` varchar(500) NOT NULL,
    `deleted` tinyint(1) NOT NULL,
    `created_at` datetime NULL,
    `updated_at` datetime NULL,
    PRIMARY KEY (`id`),
    CONSTRAINT `f_k_bovinue_metric_parameters_bovinue_metric_categories_category~` FOREIGN KEY (`category_id`) REFERENCES `bovinue_metric_categories` (`id`) ON DELETE CASCADE
);

CREATE TABLE `farms` (
    `id` int NOT NULL AUTO_INCREMENT,
    `alias` longtext NOT NULL,
    `user_id` int NOT NULL,
    `main_activity` int NOT NULL,
    `owner_dni` longtext NOT NULL,
    `created_at` datetime NULL,
    `updated_at` datetime NULL,
    PRIMARY KEY (`id`),
    CONSTRAINT `f_k_farms__users_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
);

CREATE TABLE `user_rucs` (
    `user_id` int NOT NULL,
    `ruc` varchar(11) NOT NULL,
    PRIMARY KEY (`user_id`),
    CONSTRAINT `f_k_user_rucs_users_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
);

CREATE TABLE `bovinues` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `farm_id` int NOT NULL,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    `created_at` datetime NULL,
    `updated_at` datetime NULL,
    PRIMARY KEY (`id`),
    CONSTRAINT `f_k_bovinues__farms_farm_id` FOREIGN KEY (`farm_id`) REFERENCES `farms` (`id`) ON DELETE CASCADE
);

CREATE TABLE `bovinue_health_records` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `bovinue_c_h_r_id` bigint NOT NULL,
    `bovinue_id` bigint NOT NULL,
    `start_date` datetime NOT NULL,
    `end_date` datetime NULL,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    `created_at` datetime NULL,
    `updated_at` datetime NULL,
    PRIMARY KEY (`id`),
    CONSTRAINT `f_k_bovinue_health_records_bovinue_cattle_health_records_bovinue~` FOREIGN KEY (`bovinue_c_h_r_id`) REFERENCES `bovinue_cattle_health_records` (`id`) ON DELETE CASCADE,
    CONSTRAINT `f_k_bovinue_health_records_bovinues_bovinue_id` FOREIGN KEY (`bovinue_id`) REFERENCES `bovinues` (`id`) ON DELETE CASCADE
);

CREATE TABLE `bovinue_metrics` (
    `id` bigint NOT NULL AUTO_INCREMENT,
    `bovinue_m_p_id` bigint NOT NULL,
    `bovinue_id` bigint NOT NULL,
    `date` datetime(6) NOT NULL,
    `quantity` double NOT NULL,
    `deleted` tinyint(1) NOT NULL DEFAULT FALSE,
    `created_at` datetime NULL,
    `updated_at` datetime NULL,
    PRIMARY KEY (`id`),
    CONSTRAINT `f_k_bovinue_metrics__bovinue_metric_parameters_bovinue_m_p_id` FOREIGN KEY (`bovinue_m_p_id`) REFERENCES `bovinue_metric_parameters` (`id`) ON DELETE CASCADE,
    CONSTRAINT `f_k_bovinue_metrics_bovinues_bovinue_id` FOREIGN KEY (`bovinue_id`) REFERENCES `bovinues` (`id`) ON DELETE CASCADE
);

CREATE INDEX `i_x_bovinue_health_records_bovinue_c_h_r_id` ON `bovinue_health_records` (`bovinue_c_h_r_id`);

CREATE INDEX `i_x_bovinue_health_records_bovinue_id` ON `bovinue_health_records` (`bovinue_id`);

CREATE INDEX `i_x_bovinue_metric_parameters_category_id` ON `bovinue_metric_parameters` (`category_id`);

CREATE INDEX `i_x_bovinue_metrics_bovinue_id` ON `bovinue_metrics` (`bovinue_id`);

CREATE INDEX `i_x_bovinue_metrics_bovinue_m_p_id` ON `bovinue_metrics` (`bovinue_m_p_id`);

CREATE INDEX `i_x_bovinues_farm_id` ON `bovinues` (`farm_id`);

CREATE INDEX `i_x_farms_user_id` ON `farms` (`user_id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20251008143929_InitialCreate', '9.0.9');

COMMIT;

