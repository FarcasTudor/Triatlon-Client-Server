package domain;

import java.io.Serializable;

public class ArbitruDTO implements Serializable {
    private String username;
    private String parola;

    public ArbitruDTO(String username, String parola) {
        this.username = username;
        this.parola = parola;
    }

    public String getUsername() {
        return username;
    }

    public String getParola() {
        return parola;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public void setParola(String parola) {
        this.parola = parola;
    }

    @Override
    public String toString() {
        return "ArbitruDTO{" +
                "username='" + username + '\'' +
                ", parola='" + parola + '\'' +
                '}';
    }
}
