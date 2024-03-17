package com.example.rest;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.io.FileNotFoundException;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

@RestController
@RequestMapping("/triatlon/proba")
public class ProbaController {
    private static final String template = "Hello, %s!";

    private static Long id = 12L;
    @Autowired
    private Repository<Long, Proba> probe;

    @RequestMapping("/greeting")
    public String greeting(@RequestParam(value="name", defaultValue="World") String name) {
        return String.format(template, name);
    }

    @RequestMapping(method = RequestMethod.GET)
    public Proba[] getAll() throws SQLException {
        System.out.println("Toate probele ...");
        Iterable<Proba> iterable = probe.findAll();
        List<Proba> list = new ArrayList<>();
        for (Proba proba : iterable) {
            list.add(proba);
        }
        Proba[] array = new Proba[list.size()];
        list.toArray(array);
        return array;
    }

    @RequestMapping(value = "/{id}", method = RequestMethod.GET)
    public ResponseEntity<?> getById(@PathVariable String id) throws SQLException {
        System.out.println("Get by id " + id);
        Proba proba = probe.findOne(Long.parseLong(id));
        if (proba == null)
            return new ResponseEntity<String>("Proba not found", HttpStatus.NOT_FOUND);
        else
            return new ResponseEntity<Proba>(proba, HttpStatus.OK);
    }

    @RequestMapping(method = RequestMethod.POST)
    public Proba create(@RequestBody Proba proba) throws SQLException, FileNotFoundException {
        System.out.println("Creating proba with id " + proba.getId() + " ...");
        System.out.println("id = " + id);
        proba.setId(id++);
        Proba p1 = probe.save(proba);
        return proba;
    }

    @RequestMapping(value = "/{id}", method = RequestMethod.PUT)
    public Proba update(@RequestBody Proba proba1) throws SQLException, FileNotFoundException {
        System.out.println("Updating proba ...");
        probe.update(proba1.getId(), proba1);
        return proba1;
    }

    @RequestMapping(value = "/{id}", method = RequestMethod.DELETE)
    public ResponseEntity<?> delete(@PathVariable String id) throws SQLException {
        System.out.println("Deleting proba ...");
        try {
            probe.delete(Long.parseLong(id));
            return new ResponseEntity<Proba>(HttpStatus.OK);
        }catch (Exception ex){
            return new ResponseEntity<String>(ex.getMessage(),HttpStatus.BAD_REQUEST);
        }
    }
}
