import { InjectionToken } from '@angular/core';
import { Subject } from 'rxjs';

export interface Editor {
  text: string;
}

export const EditorSubject = new InjectionToken<Subject<Editor>>('EditorSubject');
