package com.example.start;

import com.example.rest.Arbitru;
import com.example.rest.Proba;
import com.example.rest.ProbeClient;
import com.example.rest.ServiceException;
import org.springframework.web.client.RestClientException;

public class StartRestClient {
    private final static ProbeClient probeClient = new ProbeClient();

    public static void main(String[] args) {
        Arbitru arbitru = new Arbitru("nume","prenume", "username", "password");
        arbitru.setId(1L);
        Proba proba = new Proba("numeProba", 1L);
        proba.setId(1L);
        try {
            show(() -> System.out.println(probeClient.create(proba)));


            show(() -> {
                Proba[] res = probeClient.getAll();
                for (Proba p : res) {
                    System.out.println(p.getId() + ": " + p.getNumeProba());
                }
            });
        }
        catch (RestClientException e) {
            System.out.println("Exception" + e);
        }
        show(() -> System.out.println(probeClient.getById("1")));
    }

    private static void show(Runnable task) {
        try {
            task.run();
        } catch (ServiceException e) {
            System.out.println("Service exception" + e);
        }
    }
}
