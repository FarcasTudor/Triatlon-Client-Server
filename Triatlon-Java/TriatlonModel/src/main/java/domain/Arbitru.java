package domain;

import org.hibernate.annotations.GenericGenerator;

import java.lang.annotation.Annotation;
import java.util.Objects;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.Table;



@Entity
@Table( name = "arbitru" )
public class Arbitru extends domain.Entity<Long> /*extends Entity<Long>*/{
    private Long id;
    private String nume;
    private String prenume;

    private String username;
    private String parola;

    public Arbitru() {
        // this form used by Hibernate
    }
    public Arbitru(String nume, String prenume, String username, String parola) {
        this.nume = nume;
        this.prenume = prenume;
        this.username = username;
        this.parola = parola;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }
    public String getNume() {
        return nume;
    }

    public void setNume(String nume) {
        this.nume = nume;
    }

    public String getPrenume() {
        return prenume;
    }

    public void setPrenume(String prenume) {
        this.prenume = prenume;
    }

    public String getParola() {
        return parola;
    }

    public void setParola(String parola) {
        this.parola = parola;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Arbitru arbitru)) return false;
        return Objects.equals(getNume(), arbitru.getNume()) && Objects.equals(getPrenume(), arbitru.getPrenume()) && Objects.equals(getUsername(), arbitru.getUsername()) && Objects.equals(getParola(), arbitru.getParola());
    }

    @Override
    public int hashCode() {
        return Objects.hash(getNume(), getPrenume(), getUsername(), getParola());
    }

    @Override
    public String toString() {
        return "Arbitru{" +
                "nume='" + nume + '\'' +
                ", prenume='" + prenume + '\'' +
                ", parola='" + parola + '\'' +
                '}';
    }


    @Id
    @GeneratedValue(generator = "increment")
    @GenericGenerator(name="increment", strategy = "increment")
    public Long getId() {
        return id;
    }

    public void setId(Long aLong) {
        this.id = aLong;
    }

}
