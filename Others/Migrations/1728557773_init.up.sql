CREATE TYPE user_position as ENUM (
  'CUSTOMER',
  'VETERINARIAN',
  'EXECUTIVE',
  'ADMINISTRATOR'
);

CREATE TABLE
  users (
    id VARCHAR(80) PRIMARY KEY UNIQUE,
    username VARCHAR(100),
    password VARCHAR(255),
    firstname VARCHAR(100),
    lastname VARCHAR(100),
    position user_position,
    created_at TIMESTAMPTZ,
    updated_at TIMESTAMPTZ
  );

CREATE TABLE
  pets (
    id VARCHAR(100) PRIMARY KEY UNIQUE,
    user_id VARCHAR(100),
    name VARCHAR(100),
    type VARCHAR(100),
    gene VARCHAR(100),
    created_at TIMESTAMPTZ,
    updated_at TIMESTAMPTZ,
    CONSTRAINT fk_users FOREIGN KEY (user_id) REFERENCES users (id)
  );

CREATE TABLE
  sessions (
    id VARCHAR(100) PRIMARY KEY UNIQUE,
    user_id VARCHAR(100),
    ip_address VARCHAR(255),
    user_agent VARCHAR(255),
    created_at TIMESTAMPTZ,
    expired_at TIMESTAMPTZ,
    CONSTRAINT fk_users FOREIGN KEY (user_id) REFERENCES users (id)
  );
