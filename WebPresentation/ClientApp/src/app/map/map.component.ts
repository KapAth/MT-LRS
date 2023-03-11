import { Component, OnInit } from '@angular/core';
import ArcGISMap from '@arcgis/core/Map';
import MapView from '@arcgis/core/views/MapView';

@Component({
  selector: 'app-map',
  template: `
   <div id="viewDiv" style="height: 400px; width: 100%;"></div>

  `
})
export class MapComponent implements OnInit {
  ngOnInit(): void {
    const map = new ArcGISMap({
      basemap: 'streets-vector'
    });

    const view = new MapView({
      container: 'viewDiv',
      map: map,
      center: [14.511, 35.893],
      zoom: 15,
      //constraints: {
      //  snapToZoom: false
      //}
    });

    view.when(() => {
      console.log('Map loaded');
    });
  }
}
